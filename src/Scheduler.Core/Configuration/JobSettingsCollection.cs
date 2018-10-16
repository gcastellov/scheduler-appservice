using System.Configuration;

namespace Scheduler.Core.Configuration
{
    [ConfigurationCollection(typeof(CredentialSettings), AddItemName = "job", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    class JobSettingsCollection : ConfigurationElementCollection
    {
        protected override bool IsElementName(string elementName)
        {
            return elementName == "job";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new JobSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element;
        }
    }
}
