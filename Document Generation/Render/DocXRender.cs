using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text.RegularExpressions;

namespace DocumentGeneration.Render
    {
    public partial class Render
        {
        public byte[] DocXRender()
            {
            using (MemoryStream mem = new MemoryStream())
                {
                mem.Write(_content , 0 , _content.Length);
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mem , true))
                    {
                    try
                        {
                        /* 
                         * We probably want to reverse this foreach Loop to do it the other way around, rather than parsing the entire word document multiple times for each 
                         * Paramater
                         * This currently only handles strings
                         *  MultiLine
                         *  Image
                         *  Subtemplate
                         */

                        foreach (var x in _parameters)
                            {
                                ReplaceTextInPart(wordDoc.MainDocumentPart , x.ParameterName , x.ParameterValue as string);
                            foreach (var y in wordDoc.MainDocumentPart.HeaderParts)
                                { 
                                ReplaceTextInPart(y , x.ParameterName , x.ParameterValue as string);
                                }
                            foreach (var y in wordDoc.MainDocumentPart.FooterParts)
                                {                              
                                ReplaceTextInPart(y , x.ParameterName , x.ParameterValue as string);
                                }
                            }
            
                        }
                    catch(Exception ex)
                        {

                        }
                    }
                return mem.ToArray();
                }
            }

        /*
         * This doesn't work perfectly - we need to clean the document, in some instances a part could be < and the next could be "the thing" then ">" we either 
         need to handle safely and properly here or do a "Cleanup" stage beforehand.
        * Call this a few hours work. Doable.
        */
        private void ReplaceTextInPart(OpenXmlPart part , string parameter , string replacementValue)
            {
            string paramPattern = $"<{parameter}>";

            foreach (var text in part.RootElement.Descendants<Text>())
                {
                if (text.Text.Contains(paramPattern))
                    {
                    text.Text = Regex.Replace(text.Text , Regex.Escape(paramPattern) , replacementValue);
                    }
                }
            }
        }
    }
    