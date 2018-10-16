using System.Configuration;

namespace Scheduler.Core.Configuration
{
    [ConfigurationCollection(typeof(CredentialSettings), AddItemName = "credential", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class CredentialsSettingsCollection : ConfigurationElementCollection
    {
        protected override bool IsElementName(string elementName)
        {
            return elementName == "credential";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CredentialSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element;
        }
    }
}
