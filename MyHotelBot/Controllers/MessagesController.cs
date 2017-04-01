using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using Microsoft.Bot.Connector;
using Newtonsoft.Json;

using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;


using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;

namespace MyHotelBot
{
    [LuisModel("961ff663-68dd-44ec-aca5-cefe1bf04677", "4275282f071344d08c9a2c8a602db0e0")]
    [Serializable]
    public class HotelDialog: LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = "Sorry I didn't understand that";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("hi")]
        public async Task Hello(IDialogContext context, LuisResult result)
        {
            string message = "Hi, there!";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Selection")]
        public async Task selection(IDialogContext context, LuisResult result)
        {
            if (result.TryFindEntity("data", out EntityRecommendation data))
            {
                string symbol = data.Entity;
                switch (symbol)
                {
                    case "nsuite":
                        await context.PostAsync("Made a booking for a suite in New York");
                        break;
                    case "ndouble room":
                        await context.PostAsync("Made a booking for a double room in New York");
                        break;
                    case "nsingle room":
                        await context.PostAsync("Made a booking for a single room in New York");
                        break;
                    case "ssuite":
                        await context.PostAsync("Made a booking for a suite in Seattle");
                        break;
                    case "sdouble room":
                        await context.PostAsync("Made a booking for a double room in Seattle");
                        break;
                    case "ssingle room":
                        await context.PostAsync("Made a booking for a single room in Seattle");
                        break;
                    case "msuite":
                        await context.PostAsync("Made a booking for a suite in Miami");
                        break;
                    case "mdouble room":
                        await context.PostAsync("Made a booking for a double room in Miami");
                        break;
                    case "msingle room":
                        await context.PostAsync("Made a booking for a single room in Miami");
                        break;
                }

            }
        }

        [LuisIntent("bookHotel")]
        public async Task Book(IDialogContext context, LuisResult result)
        {
            if (result.TryFindEntity("builtin.geography.city", out EntityRecommendation city))
            {
                string location = city.Entity.ToLower();
                string api_response = "none";
                if (location == "new york")
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.mocky.io/v2/58d7aa3d0f0000c103dcc5d5");
                    try
                    {
                        WebResponse response = request.GetResponse();
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            api_response = reader.ReadToEnd();
                        }
                    }
                    catch (WebException ex)
                    {
                        WebResponse errorResponse = ex.Response;
                        using (Stream responseStream = errorResponse.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            String errorText = reader.ReadToEnd();
                        }
                        throw;
                    }
                    JArray objt = JArray.Parse(api_response) as JArray;
                    dynamic obj = objt;
                    var message = context.MakeMessage();
                    message.Attachments = new List<Attachment>();
                    message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                    foreach (dynamic room in obj)
                    {
                        var heroCard = new HeroCard();

                        heroCard.Title = room.type;
                        heroCard.Subtitle = "$" + room.cost + " per night";
                        heroCard.Text = room.available + " rooms available";

                        List<CardImage> images = new List<CardImage>();
                        images.Add(new CardImage(url: room.image.ToString()));
                        heroCard.Images = images;

                        List<CardAction> actions = new List<CardAction>();
                        CardAction action = new CardAction()
                        {
                            Value = "select n" + room.type,
                            Type = "imBack",
                            Title = "Select"
                        };
                        actions.Add(action);
                        heroCard.Buttons = actions;

                        message.Attachments.Add(heroCard.ToAttachment());
                    }
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                else if (location == "seattle")
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.mocky.io/v2/58d7aa730f0000d503dcc5d6");
                    try
                    {
                        WebResponse response = request.GetResponse();
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            api_response = reader.ReadToEnd();
                        }
                    }
                    catch (WebException ex)
                    {
                        WebResponse errorResponse = ex.Response;
                        using (Stream responseStream = errorResponse.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            String errorText = reader.ReadToEnd();
                        }
                        throw;
                    }
                    JArray objt = JArray.Parse(api_response) as JArray;
                    dynamic obj = objt;
                    var message = context.MakeMessage();
                    message.Attachments = new List<Attachment>();
                    message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                    foreach (dynamic room in obj)
                    {
                        var heroCard = new HeroCard();

                        heroCard.Title = room.type;
                        heroCard.Subtitle = "$" + room.cost + " per night";
                        heroCard.Text = room.available + " rooms available";

                        List<CardImage> images = new List<CardImage>();
                        images.Add(new CardImage(url: room.image.ToString()));
                        heroCard.Images = images;

                        List<CardAction> actions = new List<CardAction>();
                        CardAction action = new CardAction()
                        {
                            Value = "select s" + room.type,
                            Type = "imBack",
                            Title = "Select"
                        };
                        actions.Add(action);
                        heroCard.Buttons = actions;

                        message.Attachments.Add(heroCard.ToAttachment());
                    }
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                else if (location == "miami")
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.mocky.io/v2/58d7aadd0f0000dc03dcc5d9");
                    try
                    {
                        WebResponse response = request.GetResponse();
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            api_response = reader.ReadToEnd();
                        }
                    }
                    catch (WebException ex)
                    {
                        WebResponse errorResponse = ex.Response;
                        using (Stream responseStream = errorResponse.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            String errorText = reader.ReadToEnd();
                        }
                        throw;
                    }
                    JArray objt = JArray.Parse(api_response) as JArray;
                    dynamic obj = objt;
                    var message = context.MakeMessage();
                    message.Attachments = new List<Attachment>();
                    message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                    foreach (dynamic room in obj)
                    {
                        var heroCard = new HeroCard();

                        heroCard.Title = room.type;
                        heroCard.Subtitle = "$" + room.cost + " per night";
                        heroCard.Text = room.available + " rooms available";

                        List<CardImage> images = new List<CardImage>();
                        images.Add(new CardImage(url: room.image.ToString()));
                        heroCard.Images = images;

                        List<CardAction> actions = new List<CardAction>();
                        CardAction action = new CardAction()
                        {
                            Value = "select m" + room.type,
                            Type = "imBack",
                            Title = "Select"
                        };
                        actions.Add(action);
                        heroCard.Buttons = actions;

                        message.Attachments.Add(heroCard.ToAttachment());
                    }
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                else
                {
                    await context.PostAsync("Sorry, we don't have a hotel in " + location);
                    context.Wait(MessageReceived);
                }
                
            }
            else
            {
                string message = "No city mentioned, Please mention city in request";
                await context.PostAsync(message);
                context.Wait(MessageReceived);
            }

        }
    }

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, ()=> new HotelDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}