$(document).ready( function(){
    $(".commentReply").click(function () {
        $("#Central_block_central_col_newCommentText").val($($($(this).parent(".commentButtons")).parent(".commentBody").find("a").get(0)).attr("title").split(' ')[0] + ", ");
        $("html, body").animate({ scrollTop: $('#Central_block_central_col_newCommentText').offset().top + $('#Central_block_central_col_newCommentText').height() }, 600);
        $('#Central_block_central_col_newCommentText').focus();
    });
});