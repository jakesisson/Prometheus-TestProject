using System.Text.Json.Serialization;

namespace RestApiTests.Models
{
	public class Geo
	{
		[JsonPropertyName("lat")] public string? Lat { get; set; }
		[JsonPropertyName("lng")] public string? Lng { get; set; }
	}

	public class Address
	{
		[JsonPropertyName("street")] public string? Street { get; set; }
		[JsonPropertyName("suite")] public string? Suite { get; set; }
		[JsonPropertyName("city")] public string? City { get; set; }
		[JsonPropertyName("zipcode")] public string? Zipcode { get; set; }
		[JsonPropertyName("geo")] public Geo? Geo { get; set; }
	}

	public class Company
	{
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("catchPhrase")] public string? CatchPhrase { get; set; }
		[JsonPropertyName("bs")] public string? Bs { get; set; }
	}

	public class UserDto
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("username")] public string? Username { get; set; }
		[JsonPropertyName("email")] public string? Email { get; set; }
		[JsonPropertyName("address")] public Address? Address { get; set; }
		[JsonPropertyName("phone")] public string? Phone { get; set; }
		[JsonPropertyName("website")] public string? Website { get; set; }
		[JsonPropertyName("company")] public Company? Company { get; set; }
	}

	public class PostDto
	{
		[JsonPropertyName("userId")] public int UserId { get; set; }
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
		[JsonPropertyName("body")] public string? Body { get; set; }
	}

	public class CommentDto
	{
		[JsonPropertyName("postId")] public int PostId { get; set; }
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("email")] public string? Email { get; set; }
		[JsonPropertyName("body")] public string? Body { get; set; }
	}

	public class AlbumDto
	{
		[JsonPropertyName("userId")] public int UserId { get; set; }
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
	}

	public class PhotoDto
	{
		[JsonPropertyName("albumId")] public int AlbumId { get; set; }
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
		[JsonPropertyName("url")] public string? Url { get; set; }
		[JsonPropertyName("thumbnailUrl")] public string? ThumbnailUrl { get; set; }
	}

	public class TodoDto
	{
		[JsonPropertyName("userId")] public int UserId { get; set; }
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
		[JsonPropertyName("completed")] public bool Completed { get; set; }
	}
}
