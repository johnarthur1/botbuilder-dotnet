﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive.Testing.TestActions
{
    /// <summary>
    /// Test Script action to assert that the bots' reply matches expectations.
    /// </summary>
    [DebuggerDisplay("AssertReply{Exact ? \"[Exact]\" : string.Empty}:{GetConditionDescription()}")]
    public class AssertReply : AssertReplyActivity
    {
        /// <summary>
        /// Kind for the json object.
        /// </summary>
        [JsonProperty("$kind")]
        public new const string Kind = "Microsoft.Test.AssertReply";

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertReply"/> class.
        /// </summary>
        /// <param name="path">path.</param>
        /// <param name="line">line number.</param>
        [JsonConstructor]
        public AssertReply([CallerFilePath] string path = "", [CallerLineNumber] int line = 0)
            : base(path, line)
        {
        }

        /// <summary>
        /// Gets or sets the Text to assert.
        /// </summary>
        /// <value>The text value to look for in the reply.</value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether text should be an exact match.
        /// </summary>
        /// <value>if true, then exact match, if false, then it will be a Contains match.</value>
        [DefaultValue(true)]
        [JsonProperty("exact")]
        public bool Exact { get; set; } = true;

        /// <summary>
        /// Validates the activity.
        /// </summary>
        /// <param name="activity">The activity to be verified. </param>
        public override void ValidateReply(Activity activity)
        {
            // if we have a reply
            if (!string.IsNullOrEmpty(this.Text))
            {
                if (this.Exact)
                {
                    // Normalize line endings to work on windows and mac
                    if (activity.AsMessageActivity()?.Text.Replace("\r", string.Empty) != this.Text.Replace("\r", string.Empty))
                    {
                        throw new Exception(this.Description ?? $"Text '{activity.Text}' didn't match expected text: {this.Text}'");
                    }
                }
                else
                {
                    if (activity.AsMessageActivity()?.Text.ToLowerInvariant().Trim().Contains(this.Text.ToLowerInvariant().Trim()) == false)
                    {
                        throw new Exception(this.Description ?? $"Text '{activity.Text}' didn't match expected text: '{this.Text}'");
                    }
                }
            }

            base.ValidateReply(activity);
        }

        /// <summary>
        /// Gets the text to assert for an activity.
        /// </summary>
        /// <returns>String.</returns>
        public override string GetConditionDescription()
        {
            return $"{Text}";
        }
    }
}
