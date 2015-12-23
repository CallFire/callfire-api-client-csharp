using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Webhooks.Model
{
    public class Webhook : CallfireModel
    {
        public long? Id { get; set; }

        public bool? Enabled { get; set; }

        public string Name { get; set; }

        public ResourceType? Resource { get; set; }

        public bool? NonStrictSsl { get; set; }

        public string Fields { get; set; }

        public string Callback { get; set; }

        public string Secret { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ISet<ResourceEvent> Events { get; set; }

        private static Dictionary<ResourceType, HashSet<ResourceEvent>> supportedEvents = new Dictionary<ResourceType, HashSet<ResourceEvent>>
        {
            { ResourceType.CCC_CAMPAIGN, new HashSet<ResourceEvent> { ResourceEvent.FINISHED, ResourceEvent.STARTED, ResourceEvent.STOPPED } },
            { ResourceType.IVR_BROADCAST, new HashSet<ResourceEvent> { ResourceEvent.FINISHED, ResourceEvent.STARTED, ResourceEvent.STOPPED } },
            { ResourceType.TEXT_BROADCAST, new HashSet<ResourceEvent> { ResourceEvent.FINISHED, ResourceEvent.STARTED, ResourceEvent.STOPPED } },
            { ResourceType.VOICE_BROADCAST, new HashSet<ResourceEvent> { ResourceEvent.FINISHED, ResourceEvent.STARTED, ResourceEvent.STOPPED } }
        };

        public override string ToString()
        {
            return string.Format("[Webhook: Id={0}, Enabled={1}, Name={2}, Resource={3}, NonStrictSsl={4}, Fields={5}, Callback={6}, Secret={7}, CreatedAt={8}, UpdatedAt={9}, Events={10}]",
                Id, Enabled, Name, Resource, NonStrictSsl, Fields, Callback, Secret, CreatedAt, UpdatedAt, Events?.ToPrettyString());
        }

        public override void validate()
        {
            if (Resource != null)
            {
                foreach(ResourceEvent e in Events)
                {
                    if (!supportedEvents[(ResourceType)Resource].Contains(e))
                    {
                        var unsupportedEvent = ClientUtils.EnumMemberAttr<ResourceEvent>((ResourceEvent)e);
                        var resourceType = ClientUtils.EnumMemberAttr<ResourceType>((ResourceType)Resource);
                        var error = "Event [" + unsupportedEvent + "] is unsupported for " + resourceType + " resource, supported events are: " + 
                            supportedEvents[(ResourceType)Resource].ToPrettyString();
                        throw new ModelValidationException(new ErrorMessage { Message = error });
                    }
                }
            }
        }
    }
}

