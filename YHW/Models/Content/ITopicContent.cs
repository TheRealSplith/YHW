using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHW.Models.Content
{
    public interface ITopicContent
    {
        Int32 ID { get; set; }
        /// <summary>
        /// This image is for thumbnails, approximately 64x64
        /// </summary>
        Byte[] ThumbImage { get; set; }
        /// <summary>
        /// This image is used on content pages, 1000 x 600 pixels
        /// </summary>
        Byte[] LargeImage { get; set; }
        /// <summary>
        /// This iamge is used on content browsing pages, approximately 256 x 256
        /// </summary>
        Byte[] SmallImage { get; set; }
        /// <summary>
        /// This image is used for FaceBook Linking 377 Width x 196 Tall
        /// </summary>
        Byte[] FBImage { get; set; }
        /// <summary>
        /// Title of content, displayed along with content at all times
        /// </summary>
        String Title { get; set; }
        /// <summary>
        /// Displayed where more content is available, could be brief description
        /// </summary>
        String SubText { get; set; }
        /// <summary>
        /// Text that appears below an image
        /// </summary>
        String ImageSubText { get; set; }
        /// <summary>
        /// This is the date the content was created
        /// </summary>
        DateTime CreatedDate { get; set; }
        /// <summary>
        /// ID of Author
        /// </summary>
        Int32? AuthorID { get; set; }
        /// <summary>
        /// This returns the name of the content type
        /// </summary>
        String TypeName { get; set; }
        /// <summary>
        /// Whether or not this content is an opinion piece, we use this to indicate to
        /// users whether a piece of content
        /// </summary>
        Boolean IsOpinion { get; set; }
        /// <summary>
        /// Whether or not the content is approved for general viewing
        /// </summary>
        Boolean IsApproved { get; set; }
        /// <summary>
        /// Author of the content
        /// </summary>
        YHWProfile Author { get; set; }
    }
}
