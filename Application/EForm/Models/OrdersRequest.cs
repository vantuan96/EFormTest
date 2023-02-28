using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace EForm.Models
{
    public class OrdersRequest
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2003/05/soap-envelope", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeHeader headerField;

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeHeader Header
            {
                get
                {
                    return this.headerField;
                }
                set
                {
                    this.headerField = value;
                }
            }

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
        public partial class EnvelopeHeader
        {

            private string actionField;

            private string toField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public string Action
            {
                get
                {
                    return this.actionField;
                }
                set
                {
                    this.actionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(ElementName = "To", Namespace = "http://www.w3.org/2005/08/addressing")]
            public string To
            {
                get
                {
                    return this.toField;
                }
                set
                {
                    this.toField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public partial class EnvelopeBody
        {

            private ListMedicationOrders listMedicationOrdersField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://orionhealth.com/pharmacy/orders")]
            public ListMedicationOrders ListMedicationOrders
            {
                get
                {
                    return this.listMedicationOrdersField;
                }
                set
                {
                    this.listMedicationOrdersField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://orionhealth.com/pharmacy/orders")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://orionhealth.com/pharmacy/orders", IsNullable = false)]
        public partial class ListMedicationOrders
        {

            private ListMedicationOrdersFilter filterField;

            /// <remarks/>
            public ListMedicationOrdersFilter filter
            {
                get
                {
                    return this.filterField;
                }
                set
                {
                    this.filterField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://orionhealth.com/pharmacy/orders")]
        public partial class ListMedicationOrdersFilter
        {

            private ListMedicationOrdersFilterPatient patientField;

            /// <remarks/>
            public ListMedicationOrdersFilterPatient Patient
            {
                get
                {
                    return this.patientField;
                }
                set
                {
                    this.patientField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://orionhealth.com/pharmacy/orders")]
        public partial class ListMedicationOrdersFilterPatient
        {

            private uint valueField;

            private string systemField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes")]
            public uint Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes")]
            public string System
            {
                get
                {
                    return this.systemField;
                }
                set
                {
                    this.systemField = value;
                }
            }
        }


    }
}