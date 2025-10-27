// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function connectToSignalR() {
	var conn = new signalR.HubConnectionBuilder().withUrl("https://autobarn.dev/hub").build();
	conn.on("TellTheTechoramaFolksThis", displayNotification);
	conn.start().then(function () {
		console.log("SignalR has started.");
	}).catch(function (err) {
		console.log(err);
	});
}

function displayNotification(user, message) {
	var data = JSON.parse(message);
	var $target = $("#signalr-notifications");
	const $div = $(`
		<div>${data.Manufacturer} ${data.ModelName} (${data.Year}, ${data.Color})<br />
		PRICE: ${data.Price} ${data.Currency}<br />
		<a href="/vehicles/details/${data.Registration}">click for more!</a>
		</div>`);
	$target.prepend($div);
	$div.css("--background-color", data.Color);
	window.setTimeout(function () {
		$div.fadeOut(1000, function () {
			$div.remove();
		});
	}, 1000);
}

$(document).ready(connectToSignalR);
