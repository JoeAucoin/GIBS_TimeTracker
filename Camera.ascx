<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Camera.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.Camera" %>


<style>
#player {
  width: 100%;
  height: 100%;
}

.hidden {
  display: none;
}

.msg {
  border: 2px solid;
  border-radius: 5px;
  padding: 20px;
  text-align: center;
}
.msg.error {
  background-color: #FEE;
  border-color: red;
  color: red;
}
.msg.success {
  background-color: #EFE;
  border-color: green;
  color: green;

}








.button-group, .play-area {
  border: 1px solid grey;
  padding: 1em 1%;
  margin-bottom: 1em;
  text-align: center;
}

.button {
  padding: 0.5em;
  margin-right: 1em;
}

.play-area-sub {
  width: 47%;
  padding: 1em 1%;
  display: inline-block;
  text-align: center;
}

#capture {
  display: none;
}

#snapshot {
  display: inline-block;

}




 .hover-zoom {
    -moz-transition:all 0.3s;
    -webkit-transition:all 0.3s;
     transition:all 0.3s
 }
.hover-zoom:hover {
    -moz-transform: scale(1.1);
    -webkit-transform: scale(1.1);
     transform: scale(4.6)
 }

</style>




<!-- The buttons to control the stream -->
<div class="button-group">
  
 
  <button id="btn-capture" type="button" class="button">Capture Image</button>

    <asp:Label ID="LabelClientInfo" runat="server" Text="LabelClientInfo"></asp:Label>

     <asp:HyperLink ID="HyperLinkMakeID" runat="server" CssClass="button" Visible="true">Make ID Card</asp:HyperLink>
    
</div>





<!-- Video Element & Canvas -->

<div class="play-area">
  <div class="play-area-sub">
    
    
	<video class="hidden" id="player" autoplay="autoplay" width="620" height="460"></video>
      <h3>Camera</h3>
  </div>
  <div class="play-area-sub">
  
    <canvas id="capture" width="620" height="460"></canvas>
    <div style="width: 100%;text-align: center;"><asp:Button ID="ButtonSaveImage" runat="server" Text="Save" CssClass="button" OnClick="ButtonSaveImage_Click"  /></div>
      <div id="snapshot">Click "Capture Image" Button</div>
       <h3>Capture</h3>
  </div>
</div>

<div class="msg error hidden" id="https">The Media Device Functionality is only available over https! Go to <a href="https://codepen.io/miam84/pen/xEWvQN">https://codepen.io/miam84/pen/xEWvQN</a></div>
<div class="msg error hidden" id="no-support">Your navigator doesn't support the Media Device functionality! Check <a href="https://caniuse.com/#feat=stream">https://caniuse.com/#feat=stream</a></div>
<div class="msg error hidden" id="no-device">No media device found!</div>

<div class="play-area">Current Image: 
<asp:Image ID="ImageIDClient" runat="server" Height="100" CssClass="hover-zoom" />

</div>

<div class="button-group">
  
 
    <asp:Button ID="ButtonReturnToClientManager" runat="server" Text="Return To Client Manager" OnClick="ButtonReturnToClientManager_Click" />

    
    
</div>


<br /> &nbsp;
<br /> &nbsp;
<br /> &nbsp;
<br /> &nbsp;
<br /> &nbsp;
<br /> &nbsp;

<asp:HiddenField ID="HiddenFieldImage" runat="server" Value="" />


<script id="rendered-js">

    document.getElementById('<%= ButtonSaveImage.ClientID %>').style.visibility = "hidden";

const player = document.getElementById('player');
const httpsMessageBox = document.getElementById('https');
const noDeviceMessageBox = document.getElementById('no-device');
const noSupportMessageBox = document.getElementById('no-support');
const isHttps = !~window.location.protocol.indexOf('https');

const handleSuccess = mediaStream => {
  player.classList.remove('hidden');
  // Older browsers may not have srcObject
  if ("srcObject" in player) {
    player.srcObject = mediaStream;
  } else {
    // Avoid using this in new browsers, as it is deprecated and will be removed
    player.src = window.URL.createObjectURL(mediaStream);
  }
  player.onloadedmetadata = e => player.play();
};


if (isHttps) {
  httpsMessageBox.classList.remove('hidden');
} else if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
  navigator.mediaDevices.
  getUserMedia({
    audio: false,
      video: true
  }).

  then(handleSuccess).
  catch(err => {
    console.error(err);
    noDeviceMessageBox.classList.remove('hidden');
  });
} else {
  noSupportMessageBox.classList.remove('hidden');
}
//# sourceURL=pen.js





    // The buttons to capture and save the image
   
   
    var btnCapture = document.getElementById("btn-capture");

    // The stream & capture
    var stream = document.getElementById("player");
    var capture = document.getElementById("capture");
    var snapshot = document.getElementById("snapshot");

    // The video stream
    var cameraStream = null;

    // Attach listeners
   
    
    btnCapture.addEventListener("click", captureSnapshot);

    // Start Streaming
    function startStreaming() {

        var mediaSupport = 'mediaDevices' in navigator;

        if (mediaSupport && null == cameraStream) {

            navigator.mediaDevices.getUserMedia({ video: true })
                .then(function (mediaStream) {

                    cameraStream = mediaStream;

                    stream.srcObject = mediaStream;

                    stream.play();
                })
                .catch(function (err) {

                    console.log("Unable to access camera: " + err);
                });
        }
        else {

            alert('Your browser does not support media devices.');

            return;
        }
    }



    function captureSnapshot() {

        if (null != stream) {

            var ctx = capture.getContext('2d');
            var img = new Image();
            var myImage = document.getElementById('<%= HiddenFieldImage.ClientID %>'); 

            ctx.drawImage(stream, 0, 0, capture.width, capture.height);

            myImage.value = capture.toDataURL("image/png");

            img.src = capture.toDataURL("image/png", 1.0);
            img.width = 620;
            img.height = 460;
            img.alt = "";
            //player.width  
            document.getElementById('<%= ButtonSaveImage.ClientID %>').style.visibility = "visible";
            snapshot.innerHTML = "<p style='text-align: center;'>Image created . . . click Save to commit.</p>";

            snapshot.appendChild(img);
        }
    }

 

</script>