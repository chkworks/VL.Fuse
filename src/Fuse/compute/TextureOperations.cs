﻿using System.Collections.Generic;
using Stride.Graphics;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.compute
{

    public class ComputeTextureGet<TIndex,T> : ShaderNode<T> where T : struct
    {
        private ShaderNode<Texture> _texture;
        private ShaderNode<TIndex> _index;
        
        public ComputeTextureGet(NodeContext nodeContext, ShaderNode<T> theDefault) : base( nodeContext,"getTextureValue",  theDefault)
        {
            
        }

        public void SetInputs(ShaderNode<Texture> theTexture, ShaderNode<TIndex> theIndex)
        {
            _texture = theTexture;
            _index = theIndex;
            SetInputs(new List<AbstractShaderNode>{theTexture,theIndex});
        }

        protected override string SourceTemplate()
        {
            const string shaderCode = "${resultType} ${resultName} = ${textureName}[${index}];";
            return ShaderNodesUtil.Evaluate(shaderCode,new Dictionary<string, string>()
            {
                {"textureName", _texture.ID},
                {"index", _index.ID}
            });
        }
    }

    public class ComputeTextureSet<TIndex, T> : ShaderNode<GpuVoid>, IComputeVoid where T : struct
    {
        private ShaderNode<Texture> _texture;
        private AbstractShaderNode _index;
        private ShaderNode<T> _value;
    
        public ComputeTextureSet(NodeContext nodeContext) : base( nodeContext, "setTextureValue")
        {
            
        }

        public void SetInputs(
            ShaderNode<Texture> theTexture, 
            ShaderNode<TIndex> theIndex, 
            ShaderNode<T> theValue)
        {
            _texture = theTexture;
            _index = theIndex;
            _value = theValue;
            
            SetInputs(new List<AbstractShaderNode>{theTexture,theIndex,theValue});
        }
        
        protected override Dictionary<string, string> CreateTemplateMap()
        {
            return new Dictionary<string, string>();
        }
        
        protected override string GenerateDefaultSource()
        {
            return "";
        }

        protected override string SourceTemplate()
        {
            if (_texture == null)
            {
                return GenerateDefaultSource();
            }
            
            const string shaderCode = "${textureName}[${index}] = ${value};";
            return ShaderNodesUtil.Evaluate(shaderCode,new Dictionary<string, string>()
            {
                {"textureName", _texture.ID},
                {"index", _index.ID},
                {"value", _value.ID}
            });
        }
    }
    
    public class ComputeTextureAbstractSet : ShaderNode<GpuVoid> 
    {
        private ShaderNode<Texture> _texture;
        private AbstractShaderNode _index;
        private AbstractShaderNode _value;
    
        public ComputeTextureAbstractSet(NodeContext nodeContext) : base( nodeContext, "setTextureValue")
        {
           
        }

        public void SetInputs(ShaderNode<Texture> theTexture, 
            AbstractShaderNode theIndex,
            AbstractShaderNode theValue)
        {
            _texture = theTexture;
            _index = theIndex;
            _value = theValue;
            
            SetInputs(new List<AbstractShaderNode>{theTexture,theIndex,theValue});
        }
        
        protected override Dictionary<string, string> CreateTemplateMap()
        {
            return new Dictionary<string, string>();
        }
        
        protected override string GenerateDefaultSource()
        {
            return "";
        }

        protected override string SourceTemplate()
        {
            const string shaderCode = "${textureName}[${index}] = ${value};";
            return ShaderNodesUtil.Evaluate(shaderCode,new Dictionary<string, string>()
            {
                {"textureName", _texture.ID},
                {"index", _index.ID},
                {"value", _value.ID}
            });
        }
    }
}