using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class OrdersResponse
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

            private Action actionField;

            private ResponseHeader responseHeaderField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public Action Action
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/Amalga/HIS/V60/Framework")]
            public ResponseHeader ResponseHeader
            {
                get
                {
                    return this.responseHeaderField;
                }
                set
                {
                    this.responseHeaderField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
        public partial class Action
        {

            private byte mustUnderstandField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public byte mustUnderstand
            {
                get
                {
                    return this.mustUnderstandField;
                }
                set
                {
                    this.mustUnderstandField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts", IsNullable = false)]
        public partial class MedicationOrder
        {

            private object administrationInstructionField;

            private MedicationOrderAdministrationVerb administrationVerbField;

            private string brandLocalNameField;

            private string brandOtherNameField;

            private MedicationOrderDosageFormulation dosageFormulationField;

            private MedicationOrderDoseEntryType doseEntryTypeField;

            private object drugFilesField;

            private MedicationOrderDuration durationField;

            private MedicationOrderFrequency frequencyField;

            private string genericLocalNameField;

            private string genericOtherNameField;

            private object holdOnField;

            private string medicationOrderIdField;

            private MedicationOrderMedicationProfileType medicationProfileTypeField;

            private bool ongoingPrescriptionField;

            private MedicationOrderOrderType orderTypeField;

            private object otherNamesField;

            private MedicationOrderPatient patientField;

            private MedicationOrderPharmacyProduct pharmacyProductField;

            private MedicationOrderPrescriber prescriberField;

            private System.DateTime prescriptionMadeOnField;

            private object prescriptionTypeField;

            private bool prnField;

            private MedicationOrderPrnReason prnReasonField;

            private MedicationOrderRoute routeField;

            private uint rxItemNumberField;

            private uint rxNumberField;

            private MedicationOrderRxQuantity rxQuantityField;

            private object slidingScaleDosesField;

            private string sourceField;

            private MedicationOrderStandardDose standardDoseField;

            private string startDateTimeField;

            private MedicationOrderStatus statusField;

            private string stopDateTimeField;

            private MedicationOrderSubRoute subRouteField;

            private MedicationOrderSupplyStatus supplyStatusField;

            private object targetedProblemField;

            private object therapeuticGroupingField;

            private object titrationDosesField;

            private bool untilDiscontinuedField;

            private object variableDosesField;

            private MedicationOrderVisit visitField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object AdministrationInstruction
            {
                get
                {
                    return this.administrationInstructionField;
                }
                set
                {
                    this.administrationInstructionField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderAdministrationVerb AdministrationVerb
            {
                get
                {
                    return this.administrationVerbField;
                }
                set
                {
                    this.administrationVerbField = value;
                }
            }

            /// <remarks/>
            public string BrandLocalName
            {
                get
                {
                    return this.brandLocalNameField;
                }
                set
                {
                    this.brandLocalNameField = value;
                }
            }

            /// <remarks/>
            public string BrandOtherName
            {
                get
                {
                    return this.brandOtherNameField;
                }
                set
                {
                    this.brandOtherNameField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderDosageFormulation DosageFormulation
            {
                get
                {
                    return this.dosageFormulationField;
                }
                set
                {
                    this.dosageFormulationField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderDoseEntryType DoseEntryType
            {
                get
                {
                    return this.doseEntryTypeField;
                }
                set
                {
                    this.doseEntryTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object DrugFiles
            {
                get
                {
                    return this.drugFilesField;
                }
                set
                {
                    this.drugFilesField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderDuration Duration
            {
                get
                {
                    return this.durationField;
                }
                set
                {
                    this.durationField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderFrequency Frequency
            {
                get
                {
                    return this.frequencyField;
                }
                set
                {
                    this.frequencyField = value;
                }
            }

            /// <remarks/>
            public string GenericLocalName
            {
                get
                {
                    return this.genericLocalNameField;
                }
                set
                {
                    this.genericLocalNameField = value;
                }
            }

            /// <remarks/>
            public string GenericOtherName
            {
                get
                {
                    return this.genericOtherNameField;
                }
                set
                {
                    this.genericOtherNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object HoldOn
            {
                get
                {
                    return this.holdOnField;
                }
                set
                {
                    this.holdOnField = value;
                }
            }

            /// <remarks/>
            public string MedicationOrderId
            {
                get
                {
                    return this.medicationOrderIdField;
                }
                set
                {
                    this.medicationOrderIdField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderMedicationProfileType MedicationProfileType
            {
                get
                {
                    return this.medicationProfileTypeField;
                }
                set
                {
                    this.medicationProfileTypeField = value;
                }
            }

            /// <remarks/>
            public bool OngoingPrescription
            {
                get
                {
                    return this.ongoingPrescriptionField;
                }
                set
                {
                    this.ongoingPrescriptionField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderOrderType OrderType
            {
                get
                {
                    return this.orderTypeField;
                }
                set
                {
                    this.orderTypeField = value;
                }
            }

            /// <remarks/>
            public object OtherNames
            {
                get
                {
                    return this.otherNamesField;
                }
                set
                {
                    this.otherNamesField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderPatient Patient
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

            /// <remarks/>
            public MedicationOrderPharmacyProduct PharmacyProduct
            {
                get
                {
                    return this.pharmacyProductField;
                }
                set
                {
                    this.pharmacyProductField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderPrescriber Prescriber
            {
                get
                {
                    return this.prescriberField;
                }
                set
                {
                    this.prescriberField = value;
                }
            }

            /// <remarks/>
            public System.DateTime PrescriptionMadeOn
            {
                get
                {
                    return this.prescriptionMadeOnField;
                }
                set
                {
                    this.prescriptionMadeOnField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object PrescriptionType
            {
                get
                {
                    return this.prescriptionTypeField;
                }
                set
                {
                    this.prescriptionTypeField = value;
                }
            }

            /// <remarks/>
            public bool Prn
            {
                get
                {
                    return this.prnField;
                }
                set
                {
                    this.prnField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderPrnReason PrnReason
            {
                get
                {
                    return this.prnReasonField;
                }
                set
                {
                    this.prnReasonField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderRoute Route
            {
                get
                {
                    return this.routeField;
                }
                set
                {
                    this.routeField = value;
                }
            }

            /// <remarks/>
            public uint RxItemNumber
            {
                get
                {
                    return this.rxItemNumberField;
                }
                set
                {
                    this.rxItemNumberField = value;
                }
            }

            /// <remarks/>
            public uint RxNumber
            {
                get
                {
                    return this.rxNumberField;
                }
                set
                {
                    this.rxNumberField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderRxQuantity RxQuantity
            {
                get
                {
                    return this.rxQuantityField;
                }
                set
                {
                    this.rxQuantityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object SlidingScaleDoses
            {
                get
                {
                    return this.slidingScaleDosesField;
                }
                set
                {
                    this.slidingScaleDosesField = value;
                }
            }

            /// <remarks/>
            public string Source
            {
                get
                {
                    return this.sourceField;
                }
                set
                {
                    this.sourceField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderStandardDose StandardDose
            {
                get
                {
                    return this.standardDoseField;
                }
                set
                {
                    this.standardDoseField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public String StartDateTime
            {
                get
                {
                    return this.startDateTimeField;
                }
                set
                {
                    this.startDateTimeField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderStatus Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string StopDateTime
            {
                get
                {
                    return this.stopDateTimeField;
                }
                set
                {
                    this.stopDateTimeField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderSubRoute SubRoute
            {
                get
                {
                    return this.subRouteField;
                }
                set
                {
                    this.subRouteField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderSupplyStatus SupplyStatus
            {
                get
                {
                    return this.supplyStatusField;
                }
                set
                {
                    this.supplyStatusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object TargetedProblem
            {
                get
                {
                    return this.targetedProblemField;
                }
                set
                {
                    this.targetedProblemField = value;
                }
            }

            /// <remarks/>
            public object TherapeuticGrouping
            {
                get
                {
                    return this.therapeuticGroupingField;
                }
                set
                {
                    this.therapeuticGroupingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object TitrationDoses
            {
                get
                {
                    return this.titrationDosesField;
                }
                set
                {
                    this.titrationDosesField = value;
                }
            }

            /// <remarks/>
            public bool UntilDiscontinued
            {
                get
                {
                    return this.untilDiscontinuedField;
                }
                set
                {
                    this.untilDiscontinuedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object VariableDoses
            {
                get
                {
                    return this.variableDosesField;
                }
                set
                {
                    this.variableDosesField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderVisit Visit
            {
                get
                {
                    return this.visitField;
                }
                set
                {
                    this.visitField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderAdministrationVerb
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderDosageFormulation
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderDoseEntryType
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderDuration
        {

            private string durationField;

            private MedicationOrderDurationDurationUnit durationUnitField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string Duration
            {
                get
                {
                    return this.durationField;
                }
                set
                {
                    this.durationField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderDurationDurationUnit DurationUnit
            {
                get
                {
                    return this.durationUnitField;
                }
                set
                {
                    this.durationUnitField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderDurationDurationUnit
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderFrequency
        {

            private string administrationAllowanceAfterScheduleField;

            private string administrationAllowanceBeforeScheduleField;

            private string administrationOverdueThresholdField;

            private MedicationOrderFrequencyFrequency frequencyField;

            private MedicationOrderFrequencyFrequencyQualifier frequencyQualifierField;

            private string minimumGapBetweenDosesField;

            private string[] scheduledTimesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string AdministrationAllowanceAfterSchedule
            {
                get
                {
                    return this.administrationAllowanceAfterScheduleField;
                }
                set
                {
                    this.administrationAllowanceAfterScheduleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string AdministrationAllowanceBeforeSchedule
            {
                get
                {
                    return this.administrationAllowanceBeforeScheduleField;
                }
                set
                {
                    this.administrationAllowanceBeforeScheduleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string AdministrationOverdueThreshold
            {
                get
                {
                    return this.administrationOverdueThresholdField;
                }
                set
                {
                    this.administrationOverdueThresholdField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderFrequencyFrequency Frequency
            {
                get
                {
                    return this.frequencyField;
                }
                set
                {
                    this.frequencyField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderFrequencyFrequencyQualifier FrequencyQualifier
            {
                get
                {
                    return this.frequencyQualifierField;
                }
                set
                {
                    this.frequencyQualifierField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string MinimumGapBetweenDoses
            {
                get
                {
                    return this.minimumGapBetweenDosesField;
                }
                set
                {
                    this.minimumGapBetweenDosesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays", DataType = "duration", IsNullable = false)]
            public string[] ScheduledTimes
            {
                get
                {
                    return this.scheduledTimesField;
                }
                set
                {
                    this.scheduledTimesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderFrequencyFrequency
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderFrequencyFrequencyQualifier
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderMedicationProfileType
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderOrderType
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderPatient
        {

            private string valueField;

            private object systemField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes")]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes", IsNullable = true)]
            public object System
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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderPharmacyProduct
        {

            private object clarityField;

            private object coatingField;

            private object colorField;

            private object formField;

            private object imageField;

            private object imprintText1Field;

            private object imprintText2Field;

            private string itemCodeField;

            private object scoringField;

            private object shapeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Clarity
            {
                get
                {
                    return this.clarityField;
                }
                set
                {
                    this.clarityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Coating
            {
                get
                {
                    return this.coatingField;
                }
                set
                {
                    this.coatingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Color
            {
                get
                {
                    return this.colorField;
                }
                set
                {
                    this.colorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Form
            {
                get
                {
                    return this.formField;
                }
                set
                {
                    this.formField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Image
            {
                get
                {
                    return this.imageField;
                }
                set
                {
                    this.imageField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object ImprintText1
            {
                get
                {
                    return this.imprintText1Field;
                }
                set
                {
                    this.imprintText1Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object ImprintText2
            {
                get
                {
                    return this.imprintText2Field;
                }
                set
                {
                    this.imprintText2Field = value;
                }
            }

            /// <remarks/>
            public string ItemCode
            {
                get
                {
                    return this.itemCodeField;
                }
                set
                {
                    this.itemCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Scoring
            {
                get
                {
                    return this.scoringField;
                }
                set
                {
                    this.scoringField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Shape
            {
                get
                {
                    return this.shapeField;
                }
                set
                {
                    this.shapeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderPrescriber
        {

            private Id idField;

            private Name nameField;

            private object otherNameField;

            private object roleField;

            private SpecialtiesSpecialty[] specialtiesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public Id Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public Name Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public object OtherName
            {
                get
                {
                    return this.otherNameField;
                }
                set
                {
                    this.otherNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public object Role
            {
                get
                {
                    return this.roleField;
                }
                set
                {
                    this.roleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            [System.Xml.Serialization.XmlArrayItemAttribute("Specialty", IsNullable = false)]
            public SpecialtiesSpecialty[] Specialties
            {
                get
                {
                    return this.specialtiesField;
                }
                set
                {
                    this.specialtiesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = false)]
        public partial class Id
        {

            private string valueField;

            private object systemField;

            private string identityTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes")]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes", IsNullable = true)]
            public object System
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

            /// <remarks/>
            public string IdentityType
            {
                get
                {
                    return this.identityTypeField;
                }
                set
                {
                    this.identityTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = false)]
        public partial class Name
        {

            private object commonNameField;

            private string firstNameField;

            private string lastNameField;

            private string middleNameField;

            private object suffixField;

            private object titleField;

            /// <remarks/>
            public object CommonName
            {
                get
                {
                    return this.commonNameField;
                }
                set
                {
                    this.commonNameField = value;
                }
            }

            /// <remarks/>
            public string FirstName
            {
                get
                {
                    return this.firstNameField;
                }
                set
                {
                    this.firstNameField = value;
                }
            }

            /// <remarks/>
            public string LastName
            {
                get
                {
                    return this.lastNameField;
                }
                set
                {
                    this.lastNameField = value;
                }
            }

            /// <remarks/>
            public string MiddleName
            {
                get
                {
                    return this.middleNameField;
                }
                set
                {
                    this.middleNameField = value;
                }
            }

            /// <remarks/>
            public object Suffix
            {
                get
                {
                    return this.suffixField;
                }
                set
                {
                    this.suffixField = value;
                }
            }

            /// <remarks/>
            public object Title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
        public partial class SpecialtiesSpecialty
        {

            private bool isClinicianMainSpecialtyField;

            private SpecialtiesSpecialtyPrimarySpecialty primarySpecialtyField;

            private string shortCodeField;

            private SpecialtiesSpecialtySubspecialty subspecialtyField;

            /// <remarks/>
            public bool IsClinicianMainSpecialty
            {
                get
                {
                    return this.isClinicianMainSpecialtyField;
                }
                set
                {
                    this.isClinicianMainSpecialtyField = value;
                }
            }

            /// <remarks/>
            public SpecialtiesSpecialtyPrimarySpecialty PrimarySpecialty
            {
                get
                {
                    return this.primarySpecialtyField;
                }
                set
                {
                    this.primarySpecialtyField = value;
                }
            }

            /// <remarks/>
            public string ShortCode
            {
                get
                {
                    return this.shortCodeField;
                }
                set
                {
                    this.shortCodeField = value;
                }
            }

            /// <remarks/>
            public SpecialtiesSpecialtySubspecialty Subspecialty
            {
                get
                {
                    return this.subspecialtyField;
                }
                set
                {
                    this.subspecialtyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
        public partial class SpecialtiesSpecialtyPrimarySpecialty
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            public string Value
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
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
        public partial class SpecialtiesSpecialtySubspecialty
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            public string Value
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
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderPrnReason
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderRoute
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderRxQuantity
        {

            private decimal quantityField;

            private MedicationOrderRxQuantityUnitOfMeasurement unitOfMeasurementField;

            /// <remarks/>
            public decimal Quantity
            {
                get
                {
                    return this.quantityField;
                }
                set
                {
                    this.quantityField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderRxQuantityUnitOfMeasurement UnitOfMeasurement
            {
                get
                {
                    return this.unitOfMeasurementField;
                }
                set
                {
                    this.unitOfMeasurementField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderRxQuantityUnitOfMeasurement
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderStandardDose
        {

            private decimal administrationQuantityHighField;

            private decimal administrationQuantityLowField;

            private MedicationOrderStandardDoseAdministrationUnitOfMeasure administrationUnitOfMeasureField;

            private decimal dosageStrengthHighField;

            private decimal dosageStrengthLowField;

            private MedicationOrderStandardDoseDosageUnitOfMeasure dosageUnitOfMeasureField;

            /// <remarks/>
            public decimal AdministrationQuantityHigh
            {
                get
                {
                    return this.administrationQuantityHighField;
                }
                set
                {
                    this.administrationQuantityHighField = value;
                }
            }

            /// <remarks/>
            public decimal AdministrationQuantityLow
            {
                get
                {
                    return this.administrationQuantityLowField;
                }
                set
                {
                    this.administrationQuantityLowField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderStandardDoseAdministrationUnitOfMeasure AdministrationUnitOfMeasure
            {
                get
                {
                    return this.administrationUnitOfMeasureField;
                }
                set
                {
                    this.administrationUnitOfMeasureField = value;
                }
            }

            /// <remarks/>
            public decimal DosageStrengthHigh
            {
                get
                {
                    return this.dosageStrengthHighField;
                }
                set
                {
                    this.dosageStrengthHighField = value;
                }
            }

            /// <remarks/>
            public decimal DosageStrengthLow
            {
                get
                {
                    return this.dosageStrengthLowField;
                }
                set
                {
                    this.dosageStrengthLowField = value;
                }
            }

            /// <remarks/>
            public MedicationOrderStandardDoseDosageUnitOfMeasure DosageUnitOfMeasure
            {
                get
                {
                    return this.dosageUnitOfMeasureField;
                }
                set
                {
                    this.dosageUnitOfMeasureField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderStandardDoseAdministrationUnitOfMeasure
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderStandardDoseDosageUnitOfMeasure
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderStatus
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderSubRoute
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderSupplyStatus
        {

            private string valueField;

            private string namespaceField;

            private string localDescriptionField;

            private string otherDescriptionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string LocalDescription
            {
                get
                {
                    return this.localDescriptionField;
                }
                set
                {
                    this.localDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = true)]
            public string OtherDescription
            {
                get
                {
                    return this.otherDescriptionField;
                }
                set
                {
                    this.otherDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts")]
        public partial class MedicationOrderVisit
        {

            private string valueField;

            private object systemField;

            private uint displayIdField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes")]
            public string Value
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes", IsNullable = true)]
            public object System
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

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
            public uint DisplayId
            {
                get
                {
                    return this.displayIdField;
                }
                set
                {
                    this.displayIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/Amalga/HIS/V60/Framework")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/Amalga/HIS/V60/Framework", IsNullable = false)]
        public partial class ResponseHeader
        {

            private bool isCompressedField;

            private System.DateTime operationInvokeEndField;

            private System.DateTime operationInvokeStartField;

            private string operationInvokeTimeField;

            private byte requestCompressedLengthField;

            private string requestDeserializeTimeField;

            private byte requestUncompressedLengthField;

            private byte responseCompressedLengthField;

            private string responseSerializeTimeField;

            private byte responseUncompressedLengthField;

            private object serverMachineNameField;

            /// <remarks/>
            public bool IsCompressed
            {
                get
                {
                    return this.isCompressedField;
                }
                set
                {
                    this.isCompressedField = value;
                }
            }

            /// <remarks/>
            public System.DateTime OperationInvokeEnd
            {
                get
                {
                    return this.operationInvokeEndField;
                }
                set
                {
                    this.operationInvokeEndField = value;
                }
            }

            /// <remarks/>
            public System.DateTime OperationInvokeStart
            {
                get
                {
                    return this.operationInvokeStartField;
                }
                set
                {
                    this.operationInvokeStartField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string OperationInvokeTime
            {
                get
                {
                    return this.operationInvokeTimeField;
                }
                set
                {
                    this.operationInvokeTimeField = value;
                }
            }

            /// <remarks/>
            public byte RequestCompressedLength
            {
                get
                {
                    return this.requestCompressedLengthField;
                }
                set
                {
                    this.requestCompressedLengthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string RequestDeserializeTime
            {
                get
                {
                    return this.requestDeserializeTimeField;
                }
                set
                {
                    this.requestDeserializeTimeField = value;
                }
            }

            /// <remarks/>
            public byte RequestUncompressedLength
            {
                get
                {
                    return this.requestUncompressedLengthField;
                }
                set
                {
                    this.requestUncompressedLengthField = value;
                }
            }

            /// <remarks/>
            public byte ResponseCompressedLength
            {
                get
                {
                    return this.responseCompressedLengthField;
                }
                set
                {
                    this.responseCompressedLengthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string ResponseSerializeTime
            {
                get
                {
                    return this.responseSerializeTimeField;
                }
                set
                {
                    this.responseSerializeTimeField = value;
                }
            }

            /// <remarks/>
            public byte ResponseUncompressedLength
            {
                get
                {
                    return this.responseUncompressedLengthField;
                }
                set
                {
                    this.responseUncompressedLengthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object ServerMachineName
            {
                get
                {
                    return this.serverMachineNameField;
                }
                set
                {
                    this.serverMachineNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public partial class EnvelopeBody
        {

            private ListMedicationOrdersResponse listMedicationOrdersResponseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://orionhealth.com/pharmacy/orders")]
            public ListMedicationOrdersResponse ListMedicationOrdersResponse
            {
                get
                {
                    return this.listMedicationOrdersResponseField;
                }
                set
                {
                    this.listMedicationOrdersResponseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://orionhealth.com/pharmacy/orders")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://orionhealth.com/pharmacy/orders", IsNullable = false)]
        public partial class ListMedicationOrdersResponse
        {

            private MedicationOrder[] listMedicationOrdersResultField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("MedicationOrder", Namespace = "http://schemas.datacontract.org/2004/07/Orchestral.Pharmacy.Orders.DataContracts", IsNullable = false)]
            public MedicationOrder[] ListMedicationOrdersResult
            {
                get
                {
                    return this.listMedicationOrdersResultField;
                }
                set
                {
                    this.listMedicationOrdersResultField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.orionhealth.com/2017/08/01/ws/common/directory", IsNullable = false)]
        public partial class Specialties
        {

            private SpecialtiesSpecialty[] specialtyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Specialty")]
            public SpecialtiesSpecialty[] Specialty
            {
                get
                {
                    return this.specialtyField;
                }
                set
                {
                    this.specialtyField = value;
                }
            }
        }


    }

}