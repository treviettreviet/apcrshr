using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Enum
{
    public enum RoleType
    {
        /// <summary>
        /// Management items below: 
        /// - Article
        /// - Album
        /// - News
        /// - Photo
        /// - Slider
        /// - Upload
        /// - Video
        /// </summary>
        ResourceManager = 0,
        /// <summary>
        /// Management items below:
        /// - ConferenceDeclaration
        /// - ImportantDeadline
        /// - Presentation
        /// </summary>
        ConferenceManager = 1,
        /// <summary>
        /// All resources
        /// </summary>
        Administrator = 2,
        /// <summary>
        /// Custom role
        /// </summary>
        Custom = 3
    }
}
