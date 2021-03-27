using System;
using System.Collections.Generic;
using System.Text;

namespace VkFriendsGraph.Graph
{
    public class Node <T>
    {
        public T MainObject { get; set; }
        public List<Node<T>> ChildrenNodes { get; set; }
        public Node<T> ParentNode { get; set; } = null;

        public Node(T mainObject, List<T> childrenNodes)
        {
            MainObject = mainObject;
            ChildrenNodes = new List<Node<T>>();
            foreach (var item in childrenNodes)
            {
                Node<T> node = new Node<T>(item);
                node.ParentNode = this;
                ChildrenNodes.Add(node);
            }
        }

        public Node(T mainObject)
        {
            MainObject = mainObject;
            ChildrenNodes = new List<Node<T>>();
        }

    }
}
