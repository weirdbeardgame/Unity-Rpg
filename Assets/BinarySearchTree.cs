using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;


public class TreeSerialize<T> : JsonConverter
{

    private readonly Type[] _types;

    List<T> Temp;

    List<List<T>> TempTrees;

    public TreeSerialize(params Type[] types)
    {
        _types = types;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {

        TNode<T> Tree = value as TNode<T>;

        TNode<T> ScratchPad = Tree;

        List<TNode<T>> TreeList = new List<TNode<T>>();

        writer.WriteStartArray();

        serializer.Serialize(writer, Tree.Data);

        if (ScratchPad.Parent != null)
        {
            TreeList.Add(ScratchPad.Parent);
            //serializer.Serialize(writer, ScratchPad.Parent);
        }

        if (ScratchPad.Left != null)
        {
            TreeList.Add(ScratchPad.Left);
            ScratchPad = ScratchPad.Left;
            //serializer.Serialize(writer, ScratchPad.Left);
            //WriteJson(writer, ScratchPad.Left, serializer);

        }

        if (ScratchPad.Right != null)
        {
            TreeList.Add(ScratchPad.Right);
            ScratchPad = ScratchPad.Right;
            //serializer.Serialize(writer, ScratchPad.Right);
            //WriteJson(writer, ScratchPad.Right, serializer);
        }


        JsonConvert.SerializeObject(TreeList);

    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonToken.StartArray)
        {

            JToken token = JToken.Load(reader);

            TempTrees = token.ToObject<List<List<T>>>();

            //Temp = new List<T>();

            //Temp.Add((T)existingValue);

            /*if (!TempTrees.Contains(Temp))
            {
                TempTrees.Add(Temp);
            }*/

        }
        return TempTrees;

    }

    public override bool CanRead
    {
        get { return false; }
    }

    public override bool CanConvert(Type objectType)
    {
        return _types.Any(t => t == objectType);
    }
}

[System.Serializable]
public class BinarySearchTree<T> where T : IComparable<T>
{
    int NumElements;
    public TNode<T> Tree;

    List<T> Temp;

    bool PlaceFound;


    public int Size
    {
        get
        {
            return NumElements;
        }

        set
        {
            NumElements = value;
        }
    }

    public BinarySearchTree()
    {
        NumElements = 0;
        Tree = null;
    }

    public void Insert(T Data) // Push to tree
    {
        TNode<T> Temp = new TNode<T>(Data);
        TNode<T> ScratchPad = Tree;
        bool IsInserted = false;

        while (!IsInserted)
        {
            if (Tree == null)
            {
                Tree = Temp;
                Tree.IsRoot = true;
                NumElements += 1;
                IsInserted = true;
                return;
            }

            if (Data.CompareTo(ScratchPad.Data) < 0)
            {
                if (ScratchPad.Left == null)
                {
                    PlaceFound = true;
                    Temp.NodeID += 1;
                    ScratchPad.Left = Temp;
                    Temp.Parent = ScratchPad;
                    NumElements += 1;
                    IsInserted = true;
                    return;
                }

                else
                {
                    ScratchPad = ScratchPad.Left;
                }
            }

            else if (Data.CompareTo(ScratchPad.Data) > 0)
            {
                if (ScratchPad.Right == null)
                {
                    PlaceFound = true;
                    Temp.NodeID += 1;
                    ScratchPad.Right = Temp;
                    Temp.Parent = ScratchPad;
                    NumElements += 1;
                    IsInserted = true;
                    return;
                }
                else
                {
                    ScratchPad = ScratchPad.Right;
                }
            }
        }
    }

    public void TraversePostfix(TNode<T> _Node)
    {
        if (_Node != null)
        {
            TraversePostfix(_Node.Left);
            TraversePostfix(_Node.Right);
            Console.Write(_Node.Data + " ");
        }
    }

    void TraversePrefix(TNode<T> _Node)
    {

        if (_Node != null)
        {
            Console.Write("Data: " + _Node.Data);
            TraversePrefix(_Node.Left);
            TraversePrefix(_Node.Right);
        }

    }


    public List<T> GetData()
    {
        Temp = new List<T>();

        TNode<T> ScratchPad = Tree;

        if (ScratchPad != null)
        {
            Save(ScratchPad);
        }
        return Temp;
    }

    public TNode<T> Save(TNode<T> Node)
    {

        if (!Temp.Contains(Node.Data))
        {
            Temp.Add(Node.Data);
        }

        if (Node.Right != null)
        {
            if (!Temp.Contains(Node.Right.Data))
            {
                Temp.Add(Node.Right.Data);
            }
            return Save(Node.Right);
        }

        if (Node.Left != null)
        {
            if (!Temp.Contains(Node.Left.Data))
            {
                Temp.Add(Node.Left.Data);
            }
            return Save(Node.Left);
        }

        return Node;

    }


    public IEnumerator<T> GetEnumerator()
    {
        return enumerate(Tree).GetEnumerator();
    }

    IEnumerable<T> enumerate(TNode<T> Root)
    {
        if (Root == null)
            yield break;

        yield return Root.Data;

        foreach (var value in enumerate(Root.Left))
            yield return value;

        foreach (var value in enumerate(Root.Right))
            yield return value;
    }


    public static BinarySearchTree<T> operator ++(BinarySearchTree<T> Tree)
    {

        BinarySearchTree<T> ScratchPad;

        if (Tree == null)
        {
            return null;
        }

        else
        {
            ScratchPad = Tree;

            if (ScratchPad.Tree.Right != null)
            {

                ScratchPad.Tree.Right.Parent = ScratchPad.Tree;

                ScratchPad.Tree = ScratchPad.Tree.Right;

                if (ScratchPad.Tree.Right == null)
                {
                    return null;
                }

                if (ScratchPad.Tree.Left != null)
                {
                    while (ScratchPad.Tree.Left != null)
                    {
                        ScratchPad.Tree = ScratchPad.Tree.Left;
                    }
                }

            }

        }

        return ScratchPad;
    }

    void GrabTree(List<TNode<T>> Balance, TNode<T> _Node)
    {
        if (Balance == null)
        {
            Balance = new List<TNode<T>>();
        }

        if (_Node == null)
        {
            return;
        }

        GrabTree(Balance, _Node.Left);
        Balance.Add(_Node);
        GrabTree(Balance, _Node.Right);

    }

    public virtual TNode<T> BuildTreeUtil(List<TNode<T>> Nodes, int start, int end)
    {
        // base case
        if (start > end)
        {
            return null;
        }

        /* Get the middle element and make it root */
        int mid = (start + end) / 2;
        TNode<T> node = Nodes[mid];

        /* Using index in DataInorder traversal, construct
           left and right subtress */
        node.Left = BuildTreeUtil(Nodes, start, mid - 1);
        node.Right = BuildTreeUtil(Nodes, mid + 1, end);

        return node;
    }

    // This functions converts an unbalanced BST to a balanced BST
    public virtual TNode<T> BalanceTree(TNode<T> root)
    {
        // Store nodes of given BST in sorted order
        List<TNode<T>> nodes = new List<TNode<T>>();
        GrabTree(nodes, root);

        // Constucts BST from nodes[]
        int n = nodes.Count;
        return BuildTreeUtil(nodes, 0, n - 1);
    }

    public void TraverseInOrder(TNode<T> _Node)
    {
        if (_Node == null)
        {
            return;
        }

        TraverseInOrder(_Node.Left);
        Console.Out.WriteLine("Data: " + _Node.Data.ToString());
        TraverseInOrder(_Node.Right);
    }

    public TNode<T> Find(T Data, TNode<T> _Node)
    {

        if (_Node != null)
        {
            if (_Node.Data.CompareTo(Data) > 0)
            {
                return Find(Data, _Node.Right);
            }

            if (_Node.Data.CompareTo(Data) == 0)
            {
                return Find(Data, _Node.Left);
            }

            else
            {
                return _Node;
            }
        }

        return null;
    }

    TNode<T> Pull(T Data, TNode<T> Node) // Pull next item from tree
    {
        TNode<T> TData = Find(Data, Node);
        TNode<T> DataToReturn = TData;

        Remove(Data, TData);
        return DataToReturn;
    }

    T minValue(TNode<T> root)
    {
        T minv = root.Data;
        while (root.Left != null)
        {
            minv = root.Left.Data;
            root = root.Left;
        }
        return minv;
    }


    public TNode<T> Remove(T Data, TNode<T> _Node) // Removes one item
    {

        TNode<T> ToDelete = Find(Data, _Node);
        ToDelete = null;

        if (_Node == null)
        {
            return null;
        }


        if (Data.CompareTo(_Node.Data) == 0)
        {
            _Node.Left = Remove(Data, _Node.Left);
        }

        else if (Data.CompareTo(_Node.Data) > 0)
        {
            _Node.Right = Remove(Data, _Node.Right);
        }

        else // we deletin root
        {

            if (_Node.Left == null)
            {
                return _Node.Right;
            }

            else if (_Node.Right == null)
            {
                return _Node.Left;
            }

            _Node.Data = minValue(_Node.Right);

            _Node.Right = Remove(_Node.Data, _Node.Right);

            _Node = null;
        }

        return _Node;
    }

    void Clear() // Clears the entire tree.
    {

    }

    ~BinarySearchTree()
    {

    }
}
