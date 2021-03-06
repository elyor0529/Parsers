﻿using System.Collections.Generic;

namespace MPT.Parsers.Model
{
    /// <summary>
    /// Basic unit stripped out of a string. 
    /// Organized by a list of string units, and any child blocks that are determined by the presence of tags, such as parentheses hierarchies.
    /// </summary>
    public class Block
    {
        private List<Block> _childBlocks = new List<Block>();
        public List<Block> ChildBlocks { get { return _childBlocks; } }

        private List<string> _childText = new List<string>();
        public List<string> ChildText { get { return _childText; } }

        public Block(string input, IBlockParser parser)
        {
            input = input.Trim();
            _childText = parser.Parse(input);

            foreach (string block in _childText)
            {
                if (parser.CountBlocks(block) > 0)
                {
                    _childBlocks.Add(new Block(block, parser));
                }
            }
        }

        public override string ToString()
        {
            string message = "";
            foreach (string item in _childText)
            {
                message += item + " ";
            }
            return message.Trim();
        }
    }
}
