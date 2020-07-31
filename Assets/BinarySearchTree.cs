using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class BinarySearchTree<T> where T : IComparable<T>
{
    int NumElements;
    public TNode<T> Tree;

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


    public TNode<T> Save(TNode<T> Node)
    {
    
        while(Node.Right != null)
        {
            return Save(Node.Right);
        }

        while(Node.Left != null)
        {
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
