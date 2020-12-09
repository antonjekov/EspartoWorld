$(document).ready(function () {

    function setRating(rating, targetDiv) {
        //$('#rating-input').val(rating);
        // fill all the stars assigning the '.selected' class
        $('.rating-star').removeClass('fa-star-o').addClass('selected');
        // empty all the stars to the right of the mouse
        $('.rating-star#rating-' + rating + ' ~ .rating-star').removeClass('selected').addClass('fa-star-o');
        var itemId = $(targetDiv).find('input[Id]').val()
        console.log(itemId);

        var antiforgeryToken = $('input[name=__RequestVerificationToken]').val();
        var data = { ItemId: itemId, value: rating }
        
        $.ajax({
            type: "POST",
            url: "/api/Votes",
            data: JSON.stringify(data),
            headers: {
                'X-CSRF-TOKEN': antiforgeryToken
            },
            success: function (data) {
                $(targetDiv).find("#averageVoteValue").html(data.averageVote.toFixed(1));
                $(targetDiv).find("#votesCount").html(data.votesCount);
            },
            contentType: "application/json"
        })
    }
    
    $('.rating-star')
        .on('mouseover', function (e) {
            var rating = $(e.target).data('rating');
            // fill all the stars
            $('.rating-star').removeClass('far').addClass('fa');
            // empty all the stars to the right of the mouse
            $('.rating-star#rating-' + rating + ' ~ .rating-star').removeClass('fa').addClass('far');
        })
        .on('mouseleave', function (e) {
            // empty all the stars except those with class .selected
            $('.rating-star:not(.selected)').removeClass('fa').addClass('far');
        })
        .on('click', function (e) {
            var rating = $(e.target).data('rating');
            var targetDiv = $(e.target).parent().parent();
            
            setRating(rating, targetDiv);
        })
        .on('keyup', function (e) {
            // if spacebar is pressed while selecting a star
            if (e.keyCode === 32) {
                // set rating (same as clicking on the star)
                var rating = $(e.target).data('rating');
                setRating(rating);
            }
        });
});

