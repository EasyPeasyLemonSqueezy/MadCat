﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace NutEngine
{
    /// <summary>
    /// Класс узла графа сцены.
    /// Граф сцены представляет собой дерево.
    /// Если один узел является ребенком другого, это значит,
    /// что помимо своих преобразований к нему будут применяться 
    /// все те же преобразования, что и к родителю.
    /// Это позволяет "прицеплять" один узел к другому.
    /// </summary>
    /// <remarks>
    /// Например, мы прицепили к коту шляпу, теперь нам не нужно
    /// двигать отдельно кота и отдельно шляпу, они стали одним целым.
    /// </remarks>
    public class Node
    {
        /// Родитель узла. Если он равен null, то
        /// это корень графа сцены.
        private Node parent;

        /// Позиция, поворот и масштаб узла, хранимые
        /// в одной матрице. Она необходима для того, 
        /// чтобы можно было прицепить один узел к другому,
        /// и тогда положение ребенка будет определяется
        /// положением его родителя.
        public virtual Matrix2D Transform { get; private set; }

        public Node Parent { get { return parent; } }
        private SortedSet<Node> Children { get; }
        public virtual Vector2 Position { get; set; } /// Позиция
        public Vector2 Scale { get; set; } /// Масштаб
        public float Rotation { get; set; } /// Поворот
        public int ZOrder { get; set; } /// Z индекс
        public bool Hidden { get; set; } /// Скрыт ли узел

        protected static IComparer<Node> Comparer = new NodeComparer();

        public Node()
        {
            Transform = Matrix2D.Identity;
            Children = new SortedSet<Node>(Comparer);
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.0f;
            ZOrder = 0;
            Hidden = false;
        }

        /// <summary>
        /// Применяем к узлу преобразования родителя,
        /// затем сортируем детей по ZOrder и рисуем в таком порядке:
        /// Дети с ZOrder меньше 0 -> сам узел -> дети с ZOrder больше либо равно 0
        /// </summary>
        public virtual void Visit(SpriteBatch spriteBatch, Matrix2D currentTransform)
        {
            if (Hidden) {
                return;
            }

            /// Пересчитать матрицу.
            /// TODO: Делать это только тогда, когда необходимо,
            /// то есть изменились Scale, Rotation и Position.
            Transform = Matrix2D.CreateTRS(Position, Scale, Rotation);

            /// Перейти в новую систему координат
            currentTransform = Transform * currentTransform;

            var children = Children.GetEnumerator();
            bool next = children.MoveNext();

            /// Узлы с ZOrder меньше нуля
            while (next && children.Current.ZOrder < 0) {
                children.Current.Visit(spriteBatch, currentTransform);
                next = children.MoveNext();
            }

            /// Отрисовать сам узел при необходимости
            if (this is IDrawable) {
                var drawable = (IDrawable)this;
                drawable.Draw(spriteBatch, currentTransform);
            }

            /// Узлы с ZOrder больше либо равно нулю
            while (next) {
                children.Current.Visit(spriteBatch, currentTransform);
                next = children.MoveNext();
            }
        }

        /// <summary>
        /// Добавить ребенка.
        /// </summary>
        public void AddChild(Node child)
        {
            child.parent = this;
            Children.Add(child);
        }

        /// <summary>
        /// Удалить ребенка.
        /// </summary>
        public void RemoveChild(Node child)
        {
            child.parent = null;
            foreach (var c in child.Children) {
                c.parent = null;
            }

            Children.Remove(child);
        }

        /// <summary>
        /// Remove current node.
        /// </summary>
        public void CommitSuicide()
        {
            parent?.Children.Remove(this);
            parent = null;
            
            foreach (var child in Children) {
                child.parent = null;
            }
        }
    }
}
