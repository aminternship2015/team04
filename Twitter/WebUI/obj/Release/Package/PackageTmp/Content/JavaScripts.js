$(document).ready(function () {

    $('.button-action-edit').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.parent-tweet-container');
        var tweetId = $(this).parents('.parent-tweet-container').attr('id');
        var currentText = $(parentBlock).find('.tweet-text').text();

        $(parentBlock).find('.date-on-userpage').css('display','none');
        $(parentBlock).find('textarea').show().val(currentText).focus();
        $(parentBlock).find('.tweet-text').hide();
        $(parentBlock).find('.buttons-edit-delete').hide();
        $(parentBlock).find('.buttons-save-cancel').show();
    });

    $('.button-action-cancel').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.parent-tweet-container');
        var tweetId = $(this).parents('.parent-tweet-container').attr('id');
                
        $(parentBlock).find('.date-on-userpage').css('display', 'block');
        parentBlock.find('.tweet-text').show();
        $(parentBlock).find('textarea').hide();
        parentBlock.find('.buttons-edit-delete').show();
        parentBlock.find('.buttons-save-cancel').hide();
    });
    $('.button-action-save').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.parent-tweet-container');
        var tweetId = $(this).parents('.parent-tweet-container').attr('id');
        var currentText = $(parentBlock).find('textarea').val();

        $.get('/Tweet/Edit', { 'id': tweetId, 'text': currentText }, function (data) { });

        $(parentBlock).find('.date-on-userpage').css('display', 'block');
        $(parentBlock).find('textarea').hide();
        $(parentBlock).find('.tweet-text').text(currentText).show();
        $(parentBlock).find('.buttons-save-cancel').hide();
        $(parentBlock).find('.buttons-edit-delete').show();
    });
    $('.button-action-delete').click(function (e) {
        var tweetId = $(this).parents('.parent-tweet-container').attr('id');

        $.get('/Tweet/Delete', { 'id': tweetId }, function () { }).complete(function () {
            var decreaseTweets = +($('#tweet-badge').text()) - 1;
            $('#tweet-badge').text(decreaseTweets);
        });

        $('#' + tweetId).remove();
    });

    
    var amountScrolled = 200;

    $(window).scroll(function () {
        if ($(window).scrollTop() > amountScrolled) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });

    $('.back-to-top').click(function () {
        $('body, html').animate({
            scrollTop: 0
        }, 700);
        return false;
    });


    $('.action-follow').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.papa-container');
        var id = $(this).parents('.papa-container').attr('id');
        var increaseFollowers = +($('#followers').text()) + 1;

        $.get('/User/Follow', { 'publisherId': id, 'subsriberId': 0 }).complete(function () {
            
            $('#followers').text(increaseFollowers);
        });

        $(parentBlock).find('.action-unfollow').show();
        $(this).hide();
    });


    $('.action-unfollow').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.papa-container');
        var id = $(this).parents('.papa-container').attr('id');
        var decreaseFollowers = +($('#followers').text()) - 1;

        $.get('/User/Unfollow', { 'id': id }).complete(function () {
            $('#followers').text(decreaseFollowers);
        });

            $(parentBlock).find('.action-follow').show();
            $(this).hide();
    });


    $('.foll').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.users-table');
        var tweetId = $(this).parents('.users-table').attr('id');

        $.get('/User/Follow', { 'publisherId': tweetId, 'subsriberId': 0 });
        $(this).hide();
        $(parentBlock).find('.unfoll').css('background', '#ff6a00');
        $(parentBlock).find('.unfoll').show();
    });

    $('.unfoll').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.users-table');
        var tweetId = $(this).parents('.users-table').attr('id');

        $.get('/User/Unfollow', { 'id': tweetId });
        $(parentBlock).find('.foll').show();
        $(this).hide();

    });

    $('.unfoll').hover(function () {
        $(this).css("background-color", '#E74C3C');
    }, function () {
        $(this).css("background-color", '#FFA500');
    });

    $('.tweet-any-time').click(function () {

        $.ajax({
            url: '/Tweet/Add/',
            type: "POST",
            data: {
                Body: $('#tweet-any-time-text').val()
            },
            success: function (data) {
                $('.form-horizontal').html(data);
            }
        }).complete(function () {
            var Tweets = +($('#tweets-counter').text()) + 1;
            $('#tweets-counter').text(Tweets);

            $('#tweet-any-time-text').val('');
        });

        $('#myModal').modal("hide");
    });

    $('.view-button-edit').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.view-edit-parent');
        var Id = $(this).parents('.view-edit-parent').attr('id');
        var fNameText = $(parentBlock).find('.first-name-edit').text();
        var lNameText = $(parentBlock).find('.last-name-edit').text();
        var emailText = $(parentBlock).find('.email-edit').text();

        $(parentBlock).find('.save-cancel-buttons').show();
        $(parentBlock).find('.first-name-text').show().val(fNameText).focus();
        $(parentBlock).find('.last-name-text').show().val(lNameText);
        $(parentBlock).find('.email-text').show().val(emailText);
        $(parentBlock).find('.first-name-edit').hide();
        $(parentBlock).find('.last-name-edit').hide();
        $(parentBlock).find('.email-edit').hide();
    });

    $('.view-edit-cancel').click(function (e) {
        var clickedButton = $(e.target);
        var parentBlock = clickedButton.parents('.view-edit-parent');

        $(parentBlock).find('.save-cancel-buttons').hide();
        $(parentBlock).find('.first-name-text').hide();
        $(parentBlock).find('.last-name-text').hide();
        $(parentBlock).find('.email-text').show().hide();
        $(parentBlock).find('.first-name-edit').show();
        $(parentBlock).find('.last-name-edit').show();
        $(parentBlock).find('.email-edit').show();
    });
});
