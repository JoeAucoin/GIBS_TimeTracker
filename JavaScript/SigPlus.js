

//var tmr;

//var resetIsSupported = false;

//window.onload = function () {
//    if (IsSigWebInstalled()) {
//        resetIsSupported = GetResetSupported();
//        if (!resetIsSupported) {
//            var sigweb_link = document.createElement("a");
//            sigweb_link.href = "https://www.topazsystems.com/software/sigweb.exe";
//            sigweb_link.innerHTML = "https://www.topazsystems.com/software/sigweb.exe";

//            var note = document.getElementById("sigWebVrsnNote");
//            note.innerHTML = "There is a newer version of SigWeb available here: ";
//            note.appendChild(sigweb_link);
//        }
//    }
//    else {
//        alert("Unable to communicate with SigWeb. Please confirm that SigWeb is installed and running on this PC.");
//    }
//}

//function GetResetSupported() {
//    var minSigWebVersionResetSupport = "1.6.4.0";

//    if (isOlderSigWebVersionInstalled(minSigWebVersionResetSupport)) {
//        console.log("Old SigWeb version installed.");
//        return false;
//    }
//    return true;
//}

//function isOlderSigWebVersionInstalled(cmprVer) {
//    var sigWebVer = GetSigWebVersion();
//    if (sigWebVer != "") {
//        return isOlderVersion(cmprVer, sigWebVer);
//    } else {
//        return false;
//    }
//}

//function isOlderVersion(oldVer, newVer) {
//    const oldParts = oldVer.split('.')
//    const newParts = newVer.split('.')
//    for (var i = 0; i < newParts.length; i++) {
//        const a = parseInt(newParts[i]) || 0
//        const b = parseInt(oldParts[i]) || 0
//        if (a < b) return true
//        if (a > b) return false
//    }
//    return false;
//}

//function onSign() {
//    if (IsSigWebInstalled()) {
//        var ctx = document.getElementById('cnv').getContext('2d');
//     //   var ctx = document.getElementById('cnv').getContext('2d');
//        SetDisplayXSize(500);
//        SetDisplayYSize(100);
//        SetTabletState(0, tmr);
//        SetJustifyMode(0);
//        ClearTablet();
//        SetKeyString("0000000000000000");
//        SetEncryptionMode(0);
//        if (tmr == null) {
//            tmr = SetTabletState(1, ctx, 50);
//        }
//        else {
//            SetTabletState(0, tmr);
//            tmr = null;
//            tmr = SetTabletState(1, ctx, 50);
//        }
//    } else {
//        alert("Unable to communicate with SigWeb. Please confirm that SigWeb is installed and running on this PC.");
//    }
//}

//function onClear() {
//    ClearTablet();
//}

//function onDone() {
//    alert("Starting onDone");
//    if (NumberOfTabletPoints() == 0) {
//        alert("Please sign before continuing");
//    }
//    else {
//        SetTabletState(0, tmr); //deactivate connection

//        //NOW, EXTRACT THE SIGNATURE IN THE TOPAZ BIOMETRIC FORMAT -- SIGSTRING
//        //OR AS A BASE64-ENCODED PNG IMAGE
//        //OR BOTH

//        //********************USE THIS SECTION IF YOU WISH TO APPLY AUTOKEY TO YOUR TOPAZ SIGNATURE
//        //READ ABOUT AUTOKEY AND THE TOPAZ SIGNATURE FORMAT HERE: http://topazsystems.com/links/robustsignatures.pdf
//        //AUTOKEY IS CRITICAL TO SAVING AN eSIGN-COMPLIANT SIGNATURE
//        //AUTOKEY ONLY APPLIES TO THE TOPAZ-FORMAT SIGSTRING AND DOES NOT APPLY TO AN IMAGE OF THE SIGNATURE
//        //AUTOKEY ALLOWS THE DEVELOPER TO CRYPTOGRAPHICALLY BIND THE TOPAZ SIGNATURE TO A SET OF DATA
//        //THE PURPOSE OF THIS IS TO SHOW THAT THE SIGNATURE IS BEING APPLIED TO THE DATA YOU PASS IN USING AutoKeyAddData()
//        //IN GENERAL TOPAZ RECOMMENDS REPLICATING A TRADITIONAL 'PAPER AND PEN' APPROACH
//        //IN OTHER WORDS, IF YOU WERE TO PRINT OUT ON PAPER THE TERMS/INFORMATION THE SIGNER IS SUPPOSED TO READ AND AGREE WITH
//        //THE DATA ON THIS PAPER IS WHAT SHOULD IN WHOLE BE PASSED INTO AUTOKEYADDANSIDATA() DIGITALLY
//        //THE TOPAZ SIGSTRING IS THEN BOUND TO THIS DATA, AND CAN ONLY BE SUCCESSFULLY DECRYPTED LATER USING THIS DATA
//        //AUTOKEYADDDATA IS DEPRECATED AND REPLACED BY AUTOKEYADDANSIDATA
//        var CryptoData = "";
//        CryptoData = "This represents sample data the signer reads and is agreeing to when signing.";
//        CryptoData = CryptoData + "Concatenate all this data into a single variable.";
//        AutoKeyAddANSIData(CryptoData); //PASS THE DATA IN TO BE USED FOR AUTOKEY
//        SetEncryptionMode(2);
//        //*******END AUTOKEY SECTION
//        alert(CryptoData.toString());
//        //NOTE THAT THE AUTOKEY SECTION ABOVE IS NOT REQUIRED TO RETURN A TOPAZ SIGSTRING
//        //BUT IT IS STRONGLY RECOMMENDED IF YOU REQUIRE eSIGN COMPLIANCE
//        //RETURN THE TOPAZ-FORMAT SIGSTRING
//        SetSigCompressionMode(1);
//        //alert("KEYSTRING:" + GetKeyString());
//        document.getElementById('bioSigData').value = GetSigString();
//        document.getElementById('sigStringData').value = GetSigString();
//      //  document.Form.bioSigData.value = GetSigString();
//       // document.Form.sigStringData.value = GetSigString();
//        //THIS RETURNS THE SIGNATURE IN TOPAZ'S OWN FORMAT WITH BIOMETRIC INFORMATION

//        //TO RETURN THIS SIGSTRING LATER TO A NEW WEB PAGE USING SIGWEB, REPEAT THE CODE FROM THIS FUNCTION ABOVE STARTING AFTER SetTabletState(0, tmr)
//        //BUT AT THE END USE SetSigString() INSTEAD OF GetSigString()
//        //NOTE THAT SetSigString() TAKES 2 ARGUMENTS
//        //SetSigString(str SigString, context canvas)

//        //TO RETURN A BASE64-ENCODED PNG IMAGE OF THE SIGNATURE
//        SetImageXSize(500);
//        SetImageYSize(100);
//        SetImagePenWidth(5);
//        GetSigImageB64(SigImageCallback); //PASS IN THE FUNCTION NAME SIGWEB WILL USE TO RETURN THE FINAL IMAGE
//    }
//}


//function SigImageCallback(str) {
//    document.getElementById('sigImageData').value = str;
//  //  document.Form.sigImageData.value = str; //OBTAIN FINAL IMAGE HERE
//}

//function endDemo() {
//    ClearTablet();
//    SetTabletState(0, tmr);
//}

//function close() {
//    if (resetIsSupported) {
//        Reset();
//    } else {
//        endDemo();
//    }
//}

////Perform the following actions on
////	1. Browser Closure
////	2. Tab Closure
////	3. Tab Refresh
//window.onbeforeunload = function (evt) {
//    close();
//    clearInterval(tmr);
//    evt.preventDefault(); //For Firefox, needed for browser closure
//};