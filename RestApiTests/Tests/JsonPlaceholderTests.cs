using Xunit;
using FluentAssertions;
using RestApiTests.Models;
using System.Text.Json;
using System.Net;
using System.Collections.Generic;
using System;

public class JsonPlaceholderTests
{
    private readonly JsonPlaceholderClient _client = new();

    [Fact]
    public void Get_Post_By_Id_Should_Succeed()
    {
        var response = _client.GetPost(1);
        response.IsSuccessful.Should().BeTrue();
        response.Content.Should().Contain("userId");
    }

    [Fact]
    public void Create_Post_Should_Return_Created()
    {
        var newPost = new { title = "foo", body = "bar", userId = 1 };
        var response = _client.CreatePost(newPost);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public void Update_Post_Should_Succeed()
    {
        var updatedPost = new { id = 1, title = "updated", body = "content", userId = 1 };
        var response = _client.UpdatePost(1, updatedPost);
        response.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Patch_Post_Should_Succeed()
    {
        var patch = new { title = "patched title" };
        var response = _client.Patch("posts", 1, patch);
        response.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Get_NonExistent_Post_Should_Return_404()
    {
        var response = _client.GetPost(9999);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void Get_Post_Should_Deserialize_Correctly()
    {
        var response = _client.GetPost(1);
        var post = JsonSerializer.Deserialize<PostDto>(response.Content!);

        post.Should().NotBeNull();
        post!.Id.Should().Be(1);
        post.Title.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Get_Comment_By_Id_Should_Succeed()
    {
        var response = _client.GetSingle("comments", 1);
        response.IsSuccessful.Should().BeTrue();

        var comment = JsonSerializer.Deserialize<CommentDto>(response.Content!);
        comment.Should().NotBeNull();
        comment!.Id.Should().Be(1);
        comment.Email.Should().Contain("@");
    }

    [Fact]
    public void Get_Comments_By_PostId_Nested_Should_Succeed()
    {
        var response = _client.Get("posts/1/comments");
        response.IsSuccessful.Should().BeTrue();
        response.Content.Should().Contain("email");
    }

    [Fact]
    public void Get_Comments_By_PostId_Query_Should_Succeed()
    {
        var response = _client.GetWithQuery("comments", "postId=1");
        response.IsSuccessful.Should().BeTrue();
        response.Content.Should().Contain("name");
    }

    [Fact]
    public void Deserialize_Comments_From_Query_Should_Work()
    {
        var response = _client.GetWithQuery("comments", "postId=1");
        response.IsSuccessful.Should().BeTrue();

        var comments = JsonSerializer.Deserialize<List<CommentDto>>(response.Content!);
        comments.Should().NotBeNullOrEmpty();
        comments![0].PostId.Should().Be(1);
        comments[0].Email.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("posts")]
    [InlineData("comments")]
    [InlineData("albums")]
    [InlineData("photos")]
    [InlineData("todos")]
    [InlineData("users")]
    public void All_Resources_Should_Respond_To_GET(string resource)
    {
        var response = _client.GetSingle(resource, 1);
        response.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [InlineData("posts")]
    [InlineData("comments")]
    [InlineData("albums")]
    [InlineData("photos")]
    [InlineData("todos")]
    [InlineData("users")]
    public void All_Resources_Should_Allow_Creation(string resource)
    {
        var payload = GetValidPayload(resource);
        var response = _client.Create(resource, payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    private object GetValidPayload(string resource, int id = 1)
    {
        return resource.ToLower() switch
        {
            "posts" => new PostDto { Id = id, UserId = 1, Title = "Test Title", Body = "Test body" },
            "comments" => new CommentDto { Id = id, PostId = 1, Name = "Test User", Email = "test@example.com", Body = "Nice post!" },
            "albums" => new AlbumDto { Id = id, UserId = 1, Title = "Vacation" },
            "photos" => new PhotoDto { Id = id, AlbumId = 1, Title = "Beach", Url = "http://example.com/img.jpg", ThumbnailUrl = "http://example.com/thumb.jpg" },
            "todos" => new TodoDto { Id = id, UserId = 1, Title = "Do the thing", Completed = false },
            "users" => new UserDto
            {
                Id = id,
                Name = "John Doe",
                Username = "johndoe",
                Email = "john@example.com",
                Phone = "555-5555",
                Website = "example.com",
                Address = new Address
                {
                    Street = "Main St",
                    Suite = "Apt 1",
                    City = "Somewhere",
                    Zipcode = "12345",
                    Geo = new Geo { Lat = "0.0", Lng = "0.0" }
                },
                Company = new Company { Name = "Example Inc", CatchPhrase = "Innovation!", Bs = "tech" }
            },
            _ => throw new ArgumentException($"Unknown resource: {resource}")
        };
    }
}
