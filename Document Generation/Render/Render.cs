using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using static DocumentGeneration.HttpHelpers.HttpHelper;

namespace DocumentGeneration.Render
    {
    public partial class Render
        {
        private ILogger _log;
        private byte[] _content;
        private IEnumerable<ParameterRequest> _parameters;
        private string _renderer;

        public Render(ILogger log, byte[] content, IEnumerable<ParameterRequest> paramaters, string renderer)
            {
                _log = log;
                _content = content;
                _parameters = paramaters;
                _renderer = renderer;
            }
        public byte[] executeRender()
            {
            switch (_renderer)
                {

                case "DocX":
                        {
                        return DocXRender();
                            break;
                        }
                default:
                        {
                            throw new Exception();
                        }
                }
            }
        }
    }