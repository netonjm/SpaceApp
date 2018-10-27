/*
 * Also, the geobox which is mentioned as the input for APIs with ID 23, 24 and 25, 
is a spatio-temporal data type, which defines a region bounded by four orthogonal edges parallel to the coordinate axes. It typically uses the upper left corner point (ul) and the lower right corner point (lr) to define the geo-region.

ulx: X coordinate of the upper left point of geobox 
uly: Y coordinate of the upper left point of geobox 

lrx: X coordinate of the lower right point of geobox 
lry: Y coordinate of the lower right point of geobox

In order to specify this rectangular region with longitude and latitude, the X in this definition represents the longitude and the Y is the latitude of a geo-location.
 */

namespace SofiaApp.Host.Entities
{
	public class GeoBox
	{
		/// <summary>
		/// Gets or sets the upper left.
		/// ulx: X coordinate of the upper left point of geobox 
		/// uly: Y coordinate of the upper left point of geobox
		/// X in this definition represents the longitude
		/// </summary>
		/// <value>The upper left.</value>
		public GeoPoint UpperLeft { get; set; }

		/// <summary>
		/// Gets or sets the upper left.
		/// lrx: X coordinate of the lower right point of geobox
		/// lry: Y coordinate of the lower right point of geobox
		/// </summary>
		/// <value>The upper left.</value>
		public GeoPoint LowerRight { get; set; }
	}
}
