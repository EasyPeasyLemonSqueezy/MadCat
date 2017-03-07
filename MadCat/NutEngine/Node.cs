using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    /// <summary>
    /// Node of a scene graph.
    /// </summary>
    public class Node
    {
        public virtual Matrix2D Transform { get; private set; }

        public Node Parent { get; private set; }
        private SortedSet<Node> Children { get; }
        public virtual Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public int ZOrder { get; set; } /// Position on the Z-axis.
        public bool Hidden { get; set; }

        protected static IComparer<Node> Comparer = new NodeComparer();

        public Node()
        {
            Children = new SortedSet<Node>(Comparer);
            Initialize();
        }

        /// <summary>
        /// Initialize properties.
        /// </summary>
        protected void Initialize()
        {
            Transform = Matrix2D.Identity;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.0f;
            ZOrder = 0;
            Hidden = false;
        }

        /// <summary>
        /// Update transformation matrix.
        /// Draw node and all children if possible.
        /// </summary>
        /// <remarks>
        /// Draw order:
        /// 1. Draw children with <see cref="Node.ZOrder"/> is less than 0.
        /// 2. Draw current node.
        /// 3. Draw the rest of children (with <see cref="Node.ZOrder"/> greater than 0).
        /// </remarks>
        public virtual void Visit(SpriteBatch spriteBatch, Matrix2D currentTransform)
        {
            if (Hidden) {
                return;
            }

            /// Calculate transformation matrix.
            /// TODO: Do it only when necessary.
            Transform = Matrix2D.CreateTRS(Position, Scale, Rotation);

            /// Went to the new Space.
            /// If the world around us has been changed - we should change too.
            currentTransform = Transform * currentTransform;

            var children = Children.GetEnumerator();
            bool next = children.MoveNext();

            /// ZOrder less than 0.
            while (next && children.Current.ZOrder < 0) {
                children.Current.Visit(spriteBatch, currentTransform);
                next = children.MoveNext();
            }

            /// Draw current node if necessary.
            if (this is IDrawable drawable) {
                drawable.Draw(spriteBatch, currentTransform);
            }

            /// ZOrder greater than 0.
            while (next) {
                children.Current.Visit(spriteBatch, currentTransform);
                next = children.MoveNext();
            }
        }

        /// <summary>
        /// Add child to current node.
        /// </summary>
        /// <remarks>
        /// Be careful time complexity O(log n).
        /// </remarks>
        public void AddChild(Node child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        /// <summary>
        /// Remove child from current node.
        /// </summary>
        public void RemoveChild(Node child)
        {
            child.Parent = null;
            foreach (var c in child.Children) {
                c.Parent = null;
            }

            Children.Remove(child);
        }

        /// <summary>
        /// Remove current node.
        /// </summary>
        public void CommitSuicide()
        {
            Parent?.Children.Remove(this);
            Parent = null;
            
            foreach (var child in Children) {
                child.Parent = null;
            }
        }
    }
}
