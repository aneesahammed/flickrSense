/*
 * @file:SchemaBase
 * @brief: Strong typed schema base class.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */

namespace Microsoft.Toolkit.Uwp.Services
{
    /// <summary>
    /// Strong typed schema base class.
    /// </summary>
    public abstract class SchemaBase
    {
        /// <summary>
        /// Gets or sets identifier for strong typed record.
        /// </summary>
        public string InternalID { get; set; }
    }
}
