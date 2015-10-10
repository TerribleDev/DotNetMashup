using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces.Models;

namespace DotNetMashup.Web.Model
{
    public class TwitterData : BaseExternalData
    {
        public Lazy<Task<IOEmbedTweet>> tweet { get; set; }
    }
}