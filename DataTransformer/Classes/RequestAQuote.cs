using DataTransformer.Utils;
using System.Text.Json.Serialization;

namespace DataTransformer.Classes
{
  public class RequestAQuote : Transformable<RequestAQuote>, IHasHeaders
  {
    #region Json Properties
    [JsonPropertyName("entry_id")]
    public string entry_id { get; set; }

    [JsonPropertyName("entry_date")]
    public string entry_date { get; set; }

    [JsonPropertyName("model")]
    public string model { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("first")]
    public string first { get; set; }

    [JsonPropertyName("last")]
    public string last { get; set; }

    [JsonPropertyName("business_organization")]
    public string business_organization { get; set; }

    [JsonPropertyName("city")]
    public string city { get; set; }

    [JsonPropertyName("state")]
    public string state { get; set; }

    [JsonPropertyName("country")]
    public string country { get; set; }

    [JsonPropertyName("state_province_region")]
    public string state_province_region { get; set; }

    [JsonPropertyName("state_province_region_2")]
    public string state_province_region_2 { get; set; }

    [JsonPropertyName("postal_code")]
    public string postal_code { get; set; }

    [JsonPropertyName("phone")]
    public string phone { get; set; }

    [JsonPropertyName("email")]
    public string email { get; set; }

    [JsonPropertyName("additional_models_requested_to_quote")]
    public string additional_models_requested_to_quote { get; set; }

    [JsonPropertyName("i_would_like_to_receive_information_from_true_refrigeration_about_products_and_important_updates")]
    public string i_would_like_to_receive_information_from_true_refrigeration_about_products_and_important_updates { get; set; }

    [JsonPropertyName("user_ip")]
    public string user_ip { get; set; }
    #endregion Json Properties

    #region Transformed Properties
    private string _state_province_region;
    private string _name;
    #endregion Transformed Properties

    public override string Transform()
    {
      string[] stateProvinceRegionParts = { state, state_province_region, state_province_region_2 };
      if (stateProvinceRegionParts.Where(x => x.Length > 0).Count() > 1)
        throw new Exception($"Entry {entry_id}: unable to determine state/province/region");

      _state_province_region = string.Join("", stateProvinceRegionParts);
      
      _name = $"{first} {last}";
      if (string.IsNullOrWhiteSpace(_name))
        _name = name;

      return ToString();
    }

    public override string ToString()
    {
      return string.Join(",",
        entry_id.ToCsvField(),
        entry_date.ToCsvField(),
        model.ToCsvField(),
        _name.ToCsvField(),
        business_organization.ToCsvField(),
        city.ToCsvField(),
        _state_province_region.ToCsvField(),
        country.ToCsvField(),
        postal_code.ToCsvField(),
        phone.ToCsvField(),
        email.ToCsvField(),
        additional_models_requested_to_quote.ToCsvField(),
        i_would_like_to_receive_information_from_true_refrigeration_about_products_and_important_updates.ToCsvField(),
        user_ip.ToCsvField()
        );
    }

    public static string GetColumnHeaders()
    {
      return string.Join(",",
        "Entry Id",
        "Entry Date",
        "Model",
        "Name",
        "Business/Organization",
        "City",
        "State/Province/Region",
        "Country",
        "Postal Code",
        "Phone",
        "Email",
        "Additional Models?",
        "Receive Information?",
        "User IP"
      );
    }
  }
}
