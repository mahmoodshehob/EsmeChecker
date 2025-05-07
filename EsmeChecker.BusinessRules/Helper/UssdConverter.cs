using EsmeChecker.Models.Ussd;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static EsmeChecker.Models.Ussd.MultiRequestUssdSerXml;


namespace EsmeChecker.BusinessRules.Helper
{
	public class UssdConverter
	{
		private static MultiRequestUSSD multiRequestRE = new MultiRequestUSSD();
		private static XmlSerializer serializer = new XmlSerializer(typeof(MethodCall));

		public static async Task<MultiRequestUSSD> ParseRequest(string xmlString)
		{
			using (StringReader reader = new StringReader(xmlString))
			{
				await Task.Run(() =>
				{
					var methodCallReq = (MethodCall)serializer.Deserialize(reader);

					Struct @struct = methodCallReq.Params.Param.Values.Struct;

					foreach (var member in @struct.Member)
					{
						switch (member.Name)
						{
							case "TransactionId":
								multiRequestRE.TransactionId = member.Value.String;
								break;

							case "TransactionTime":
								if (member.Value.DateTimeIso8601 != null)
								{
									multiRequestRE.TransactionTime = DateTime.ParseExact(member.Value.DateTimeIso8601, "yyyyMMddTHH:mm:ss", CultureInfo.InvariantCulture).ToString(); ;
								}
								else
								{
									multiRequestRE.TransactionTime = DateTime.ParseExact(member.Value.String, "yyyyMMddTHH:mm:ss", CultureInfo.InvariantCulture).ToString(); ;
								}
								break;

							case "MSISDN":
								multiRequestRE.MSISDN = member.Value.String;
								break;

							case "USSDServiceCode":
								multiRequestRE.USSDServiceCode = member.Value.String;
								break;

							case "USSDRequestString":
								multiRequestRE.USSDRequestString = member.Value.String;
								break;

							case "response":
								multiRequestRE.Response = member.Value.String;
								break;
						}
					}
				}).ConfigureAwait(false);
				return multiRequestRE;
			}
		}

		public static class CreateResponses
		{
			private static string OrganizeXmlString(string xml)
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xml);

				StringBuilder stringBuilder = new StringBuilder();
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "    " // Use four spaces for indentation
				};

				using (XmlWriter writer = XmlWriter.Create(stringBuilder, settings))
				{
					xmlDoc.WriteTo(writer);
				}

				string afterOrganizeXml = stringBuilder.ToString();

				return stringBuilder.ToString();
			}

			public static string Success(MultiResponseUSSD multiResponse)
			{
				return OrganizeXmlString(
	 @"<?xml version=""1.0"" encoding=""UTF-8""?>
<methodResponse>
<params>
<param>
<value>
<struct>
<member>
<name>TransactionId</name>
<value>
<string>" + multiResponse.TransactionId + @"</string>
</value>
</member>
<member>
<name>TransactionTime</name>
<value><dateTime.iso8601>" + DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:ssK") + @"</dateTime.iso8601>
</value>
</member>
<member>
<name>USSDResponseString</name>
<value>
<string>" + multiResponse.USSDResponseString + @"</string>
</value>
</member>
<member>
<name>action</name>
<value>
<string>" + multiResponse.Action + @"</string>
</value>
</member>
</struct>
</value>
</param>
</params>
</methodResponse>");
			}

			public static string Fault(MultiResponseUSSD multiResponse)
			{
				return OrganizeXmlString(
	@"<?xml version=""1.0"" encoding=""UTF-8""?>
<methodResponse>
<fault>
<value>
 <struct>
 <member>
 <name>faultCode</name>
 <value>
 <int>" + multiResponse.ResponseCode + @"</int>
 </value>
 </member>
 <member>
 <name>faultString</name>
 <value>
<string>" + multiResponse.USSDResponseString + @"</string>
 </value>
 </member>
 </struct>
</value>
</fault>
</methodResponse>");
			}
		}
	}
}