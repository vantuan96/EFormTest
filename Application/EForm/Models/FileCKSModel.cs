using EForm.Common;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Web.UI.WebControls;

namespace EForm.Models
{
    public class FileCKSModel
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2003/05/soap-envelope", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public partial class EnvelopeBody
        {

            private API_ESIGN_SERVERResponse aPI_ESIGN_SERVERResponseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
            public API_ESIGN_SERVERResponse API_ESIGN_SERVERResponse
            {
                get
                {
                    return this.aPI_ESIGN_SERVERResponseField;
                }
                set
                {
                    this.aPI_ESIGN_SERVERResponseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
        public partial class API_ESIGN_SERVERResponse
        {

            private API_ESIGN_SERVERResponseAPI_ESIGN_SERVERResult aPI_ESIGN_SERVERResultField;

            /// <remarks/>
            public API_ESIGN_SERVERResponseAPI_ESIGN_SERVERResult API_ESIGN_SERVERResult
            {
                get
                {
                    return this.aPI_ESIGN_SERVERResultField;
                }
                set
                {
                    this.aPI_ESIGN_SERVERResultField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        public partial class API_ESIGN_SERVERResponseAPI_ESIGN_SERVERResult
        {

            private string codeField;

            private string messageField;

            private string dataField;

            /// <remarks/>
            public string code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }

            /// <remarks/>
            public string message
            {
                get
                {
                    return this.messageField;
                }
                set
                {
                    this.messageField = value;
                }
            }

            /// <remarks/>
            public string data
            {
                get
                {
                    return this.dataField;
                }
                set
                {
                    this.dataField = value;
                }
            }
        }


    }
}