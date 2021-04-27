using System;
using Newtonsoft.Json;

namespace QuickType
{
    public class HotelsResponse
    {
        [JsonProperty("data")]
        public HotelOffers[] Data { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    public class HotelOffers
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("hotel")]
        public Hotel Hotel { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("offers")]
        public Offer[] Offers { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public class Hotel
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("hotelId")]
        public string HotelId { get; set; }

        [JsonProperty("chainCode")]
        public string ChainCode { get; set; }

        [JsonProperty("dupeId")]
        public long DupeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("cityCode")]
        public string CityCode { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("hotelDistance")]
        public HotelDistance HotelDistance { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public HotelDescription Description { get; set; }

        [JsonProperty("amenities")]
        public string[] Amenities { get; set; }

        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public Media[] Media { get; set; }
    }

    public class Address
    {
        [JsonProperty("lines")]
        public string[] Lines { get; set; }

        [JsonProperty("postalCode")]
        public long PostalCode { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }

    public class Contact
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }

    public class HotelDescription
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class HotelDistance
    {
        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("distanceUnit")]
        public string DistanceUnit { get; set; }
    }

    public class Media
    {
        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }

    public class Offer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("checkInDate")]
        public DateTimeOffset CheckInDate { get; set; }

        [JsonProperty("checkOutDate")]
        public DateTimeOffset CheckOutDate { get; set; }

        [JsonProperty("rateCode")]
        public string RateCode { get; set; }

        [JsonProperty("rateFamilyEstimated", NullValueHandling = NullValueHandling.Ignore)]
        public RateFamilyEstimated RateFamilyEstimated { get; set; }

        [JsonProperty("room")]
        public Room Room { get; set; }

        [JsonProperty("guests")]
        public Guests Guests { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("policies")]
        public Policies Policies { get; set; }

        [JsonProperty("commission", NullValueHandling = NullValueHandling.Ignore)]
        public Commission Commission { get; set; }
    }

    public class Commission
    {
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public long? Amount { get; set; }

        [JsonProperty("percentage")]
        public long Percentage { get; set; }
    }

    public class Guests
    {
        [JsonProperty("adults")]
        public long Adults { get; set; }
    }

    public class Policies
    {
        [JsonProperty("holdTime", NullValueHandling = NullValueHandling.Ignore)]
        public HoldTime HoldTime { get; set; }

        [JsonProperty("guarantee", NullValueHandling = NullValueHandling.Ignore)]
        public Guarantee Guarantee { get; set; }

        [JsonProperty("paymentType")]
        public string PaymentType { get; set; }

        [JsonProperty("cancellation", NullValueHandling = NullValueHandling.Ignore)]
        public Cancellation Cancellation { get; set; }
    }

    public class Cancellation
    {
        [JsonProperty("numberOfNights", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumberOfNights { get; set; }

        [JsonProperty("deadline")]
        public DateTimeOffset Deadline { get; set; }
    }

    public class Guarantee
    {
        [JsonProperty("acceptedPayments")]
        public AcceptedPayments AcceptedPayments { get; set; }
    }

    public class AcceptedPayments
    {
        [JsonProperty("creditCards")]
        public string[] CreditCards { get; set; }

        [JsonProperty("methods")]
        public string[] Methods { get; set; }
    }

    public class HoldTime
    {
        [JsonProperty("deadline")]
        public DateTimeOffset Deadline { get; set; }
    }

    public class Price
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("base", NullValueHandling = NullValueHandling.Ignore)]
        public string Base { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("variations")]
        public Variations Variations { get; set; }
    }

    public class Variations
    {
        [JsonProperty("average", NullValueHandling = NullValueHandling.Ignore)]
        public Average Average { get; set; }

        [JsonProperty("changes")]
        public Change[] Changes { get; set; }
    }

    public class Average
    {
        [JsonProperty("base")]
        public string Base { get; set; }
    }

    public class Change
    {
        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("base", NullValueHandling = NullValueHandling.Ignore)]
        public string Base { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public string Total { get; set; }
    }

    public class RateFamilyEstimated
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Room
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typeEstimated")]
        public TypeEstimated TypeEstimated { get; set; }

        [JsonProperty("description")]
        public RoomDescription Description { get; set; }
    }

    public class RoomDescription
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class TypeEstimated
    {
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("beds", NullValueHandling = NullValueHandling.Ignore)]
        public long? Beds { get; set; }

        [JsonProperty("bedType", NullValueHandling = NullValueHandling.Ignore)]
        public string BedType { get; set; }
    }

    public class Meta
    {
        [JsonProperty("links")]
        public Links Links { get; set; }
    }

    public class Links
    {
        [JsonProperty("next")]
        public Uri Next { get; set; }
    }
}