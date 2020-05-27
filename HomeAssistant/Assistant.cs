using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using HADotNet.Core;
using HADotNet.Core.Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeAssistant
{
    public static class Assistant
    {
        public static async Task Initialize()
        {
            try
            {
                ClientFactory.Initialize("<URL of your Home Assistant instance>", Consts.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task<List<Light>> GetLights()
        {
            try
            {
                if (!ClientFactory.IsInitialized)
                {
                    await Initialize();
                }

                var statesClient = ClientFactory.GetClient<StatesClient>();
                var entityClient = ClientFactory.GetClient<EntityClient>();
                var lightEntities = await entityClient.GetEntities("light");
                var lights = new List<Light>();
                foreach (var lightEntity in lightEntities)
                {
                    var state = await statesClient.GetState(lightEntity);
                    Console.WriteLine($"The light {state.EntityId} is {state.State}");
                    var light = new Light
                    {
                        Id = lightEntity,
                        Name = state.Attributes["friendly_name"].ToString()
                    };
                    LightColor color = LightColor.Default;
                    if (state.Attributes.ContainsKey("rgb_color"))
                    {
                        var array = (state.Attributes["rgb_color"] as JArray);
                        if (array != null)
                        {
                            color = new LightColor(int.Parse(array[0].ToString()),
                                int.Parse(array[1].ToString()),
                                int.Parse(array[2].ToString()));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No");
                    }
                    light.SetInitialState(state.State == "on", color);
                    lights.Add(light);
                }

                return lights.OrderBy(l => l.Name).ToList();
            }
            catch (Exception e)
            {
                Debugger.Break();
                return new List<Light>();
            }
        }

        public static async Task<LightColor> GetColor(string entityId)
        {
            try
            {
                var statesClient = ClientFactory.GetClient<StatesClient>();
                var state = await statesClient.GetState(entityId).ConfigureAwait(false);
                if (state.Attributes.ContainsKey("rgb_color"))
                {
                    var array = (state.Attributes["rgb_color"] as JArray);
                    if (array != null)
                    {
                        return new LightColor(int.Parse(array[0].ToString()),
                            int.Parse(array[1].ToString()),
                            int.Parse(array[2].ToString()));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return LightColor.Default;
        }

        public static async Task ChangeColor(string entityId, LightColor computedColor)
        {
            var serviceClient = ClientFactory.GetClient<ServiceClient>();
            var data = new
            {
                entity_id = entityId,
                rgb_color = new List<double> {computedColor.Red, computedColor.Green, computedColor.Blue}
            };

            var serializeObject = JsonConvert.SerializeObject(data);
            var state = await serviceClient.CallService("light.turn_on", serializeObject);
        }

        public static async Task ToggleLight(string lightId)
        {
            var serviceClient = ClientFactory.GetClient<ServiceClient>();
            var data = new
            {
                entity_id = lightId
            };
            await serviceClient.CallService("light.toggle", JsonConvert.SerializeObject(data));
        }
    }
}
