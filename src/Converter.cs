using System;
using System.Linq;
using Hl7.Fhir.Model;
using HL7.Dotnetcore;

namespace Cdc.Nndss.Fhir
{
    public class Converter
    {
        /// <summary>
        /// Converts an HL7 v2.5.1 message to FHIR
        /// </summary>
        /// <param name="rawMessage">The HL7v2 message</param>
        public Patient Convert(string rawMessage)
        {
            Message message = new Message(rawMessage);
            message.ParseMessage();

            var patient = new Patient()
            {
                Active = true,
                Text = new Narrative(),
            };
            
            var identifier = new Identifier();
            identifier.Value = System.Guid.NewGuid().ToString();
            patient.Identifier.Add(identifier);

            var address = new Address();
            address.State = message.Segments("PID")[0].Fields(11).Components(4).Value;
            address.City = message.Segments("PID")[0].Fields(11).Components(3).Value;
            address.PostalCode = message.Segments("PID")[0].Fields(11).Components(5).Value;
            address.Country = message.Segments("PID")[0].Fields(11).Components(6).Value;
            address.District = message.Segments("PID")[0].Fields(11).Components(9).Value;
            patient.Address.Add(address);

            Redact(patient);

            return patient;
        }

        private void Redact(Patient patient)
        {
            patient.Name.Clear();
            patient.Telecom.Clear();
            patient.Contact.Clear();
        }
    }
}