var url = 'http://192.168.1.16:5248/';
//refresh trình duyệt
window.onload = async function() {
    await getStatusSpeak();
    await getStatusAir();
    await checkStatusBulb1();
    await checkStatusBulb2();
    await checkColorBulb2();
    await getTemp();
    await getHum();
  };
// Voice
var SpeechRecognition = SpeechRecognition ||  webkitSpeechRecognition;
const recognition = new SpeechRecognition();
const synth = window.speechSynthesis;
recognition.lang = 'vi-VI';
//recognition.continuous = false;

const microphone = document.querySelector('.microphone');

// render tieng viet trong gg chrome
//const speak = (text) => {
//     if (synth.speaking) {
//         console.error('Busy. Speaking...');
//         return;
//     }

    // const utter = new SpeechSynthesisUtterance(text);

    // utter.onend = () => {
    //     console.log('SpeechSynthesisUtterance.onend');
    // }
    // utter.onerror = (err) => {
    //     console.error('SpeechSynthesisUtterance.onerror', err);
    // }

  //  synth.speak(utter);
 //};

const handleVoice = async (text) => {
    console.log('text', text);

    // "bat/tat den 1/2" => ["bat/tat den", "1/2"]
    const handledText = text.toLowerCase();
    if (handledText.includes('bật đèn')) {
        var numberOnBulb = handledText.split('đèn')[1].trim();
        console.log(numberOnBulb)
        if ((numberOnBulb.includes('1') || numberOnBulb.includes('một')) && checkBulb1 === 0)
        {
            await bulb1OnOff();
           // speak(`đã ${text}`)
        }
        if ((numberOnBulb.includes("2") || numberOnBulb.includes("hai")) && checkBulb2 === 0)
        {
            await bulb2OnOff();
           // speak(`đã ${text}`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    if (handledText.includes('tắt đèn')) {
        var numberOffBulb = handledText.split('đèn')[1].trim();
        if ((numberOffBulb.includes("1") || numberOffBulb.includes("một")) && checkBulb1 === 1)
        {
            await bulb1OnOff();
           // speak(`đã ${text}`)
        }
        if ((numberOffBulb.includes("2") || numberOffBulb.includes("hai")) && checkBulb2 === 1)
        {
            await bulb2OnOff();
           // speak(`đã ${text}`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }

    if (handledText.includes('đèn hai') || handledText.includes('đèn 2') || handledText.includes('đen 2') || handledText.includes('đen hai')) {
        var numberOnBulb = handledText.split('hai')[1].trim() || handledText.split('2')[1].trim();
        if (numberOnBulb.includes('đỏ') && checkColor2 !== 1)
        {
            await putRestApi(1,3)
            color2[0].style.color = 'red';
            checkColor2 = 1;
            //speak(`đã ${text}`)
        }
        if ((numberOnBulb.includes("xanh lục") || numberOnBulb.includes("sanh lục") || numberOnBulb.includes("xanh nục") || numberOnBulb.includes("sanh nục")) && checkColor2 !== 2)
        {
            await putRestApi(1,18)
            color2[0].style.color = 'green';
            checkColor2 = 2;
            //speak(`đã ${text}`)
        }
        if ((numberOnBulb.includes("xanh lam") || numberOnBulb.includes("xanh nam") || numberOnBulb.includes("sanh nam") || numberOnBulb.includes("sanh lam")) && checkColor2 !== 3)
        {
            await putRestApi(1,19);
            color2[0].style.color = 'blue';
            checkColor2 = 3;
          //  speak(`đã ${text}`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    
    // bat/tat dieu hoa/loa
    if (handledText.includes('bật')) {
        var poweron = handledText.split('bật')[1].trim();
        console.log(poweron)
        if (poweron.includes("điều hòa") && statusAir === 0)
        {
            await clickPowerAir()
            statusAir = 1;
           // speak(`đã ${text}`)
        }
        if (poweron.includes("loa") && statusSpeak === 0)
        {
            await clickPowerSpeak();
            statusSpeak = 1;
           // speak(`đã ${text}`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    if (handledText.includes('tắt')) {
        var poweroff = handledText.split('tắt')[1].trim();
        if (poweroff.includes("điều hòa") && statusAir === 1)
        {
            await clickPowerAir();
            statusAir = 0;
           // speak(`đã ${text}`)
        }
        if (poweroff.includes("loa") && statusSpeak === 1)
        {
            await clickPowerSpeak()
            statusSpeak = 0;
           // speak(`đã ${text}`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    
    // voice set nhiệt độ
    if (handledText.includes('nhiệt độ')) {
        var temp = handledText.split('độ')[1].trim();
        var intTemp = Number(temp);
        await setTempAir(intTemp);
        if (intTemp >=16 && intTemp <= 30) {
            document.querySelector('.temp').innerHTML = `${intTemp}`
            //speak(`${text} độ C`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }

    // voice mode
    if (handledText.includes('chế độ')) {
        var mode = handledText.split('độ')[1].trim();      
        if (mode.includes("làm lạnh") && statusAir === 1 && modeAir !== 2)
        {
            await putRestApi(1,9);
            modeAir = 2;
            document.querySelector('.mode').innerHTML = 'Mode: COOL'
            //speak(`đã bật ${text}`)
        }
        if (mode.includes("auto") && statusAir === 1 && modeAir !== 0)
        {
            await putRestApi(1,7);
            modeAir = 0;
            document.querySelector('.mode').innerHTML = 'Mode: AUTO'
           // speak(`đã bật ${text}`)
        }
        if (mode.includes("làm khô") && statusAir === 1 && modeAir !== 3)
        {
            await putRestApi(1,10);
            modeAir = 3;
            document.querySelector('.mode').innerHTML = 'Mode: DRY'
           // speak(`đã bật ${text}`)
        }
        if (mode.includes("quạt gió") && statusAir === 1 && modeAir !== 4)
        {
            await putRestApi(1,23);
            modeAir = 4;
            document.querySelector('.mode').innerHTML = 'Mode: FAN'
           // speak(`đã bật ${text}`)
        }
        if (mode.includes("sưởi") && statusAir === 1 && modeAir !== 1)
        {
            await putRestApi(1,8);
            modeAir = 1;
            document.querySelector('.mode').innerHTML = 'Mode: HEAT'
           // speak(`đã bật ${text}`)
        }
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    // voice speed
    if (handledText.includes('tăng tốc') && statusAir === 1) {
        await clickSpeedAir();
        //speak(`đã ${text}`)
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    

    /* voice remote loa */

    // voice play pause
    if (handledText.includes('phát nhạc') && statusSpeak === 1 && playPauseSpeak === 0) {
            await clickPlayPauce();
           // speak('okay')
            document.querySelector('.voice_text').innerHTML = `${text}`;
            return;
    }
    if (handledText.includes('tạm dừng') && statusSpeak === 1 && playPauseSpeak === 1) {
            clickPlayPauce();
           // speak('đã dừng nhạc')
            document.querySelector('.voice_text').innerHTML = `${text}`;
            return;
    }

    // voice mutemute
    if (handledText.includes('mute') || handledText.includes('mua te') ) {
        clickStop();
       // speak(`đã ${text}`)
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    if (handledText.includes('unmute') && volumeSpeak === 0) {
        clickStop();
       // speak(`đã ${text}`)
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    // voice next/back bài
    if (handledText.includes('chuyển bài tiếp') && statusSpeak === 1) {
        clickNext();
      //  speak('okay')
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    if (handledText.includes('quay lại bài') && statusSpeak === 1) {
        clickBack();
     //   speak('okay')
        document.querySelector('.voice_text').innerHTML = `${text}`;
        return;
    }
    document.querySelector('.voice_text').innerHTML = `${text}`;
    //speak('Action filed');
}

microphone.addEventListener('click', async (e) => {
    e.preventDefault();

    recognition.start();
    microphone.classList.add('recording');
    setTimeout(() => {
        recognition.stop();
        microphone.classList.remove('recording');
    }, 3000);
});

// recognition.onspeechend = () => {
//     recognition.stop();
//     microphone.classList.remove('recording');
// }

// recognition.onerror = (err) => {
//     console.error(err);
//     microphone.classList.remove('recording');
// }

recognition.onresult = (e) => {
    console.log('onresult', e);
    const text = e.results[0][0].transcript;
    handleVoice(text);
}

// remote
const displayAir = document.getElementsByClassName("display");
const displaySpeak = document.getElementsByClassName("display_speak");
const volumeMute = document.getElementsByClassName("icon_mute");
const volumeMax = document.getElementsByClassName("icon_volume_max");
async function fetchData(urlFetch,type) {
    var dataFetch = await fetch(urlFetch, {
        method: type,
        headers: {
            'Content-type': 'application/json'
        }
        })
        .then(res => {
            if (res.ok) { console.log("HTTP request successful") }
            else { console.log("HTTP request unsuccessful") }
            return res
        })
        return dataFetch;
}

//var url = 'http://172.20.10.5:5248/';
var urlTemp = url + 'gettemp/1'; // lay nhiet do trong database
async function getTemp()  {
    await fetchData(urlTemp,"GET")
    .then(res => res.json())
    .then(data => document.querySelector('.temperature').innerHTML = `${data}`)
    .catch(error => error)
}
setInterval(getTemp,300000)

var urlHum = url + 'gethum/1'; // lay do am trong database
async function getHum()
{
    await fetchData(urlHum,"GET")
    .then(res => res.json())
    .then(data => document.querySelector('.humidity').innerHTML = `${data}`)
    .catch(error => error)
}
setInterval(getHum,300000);     


/* remote bulbs */
// bat tat den 1
async function bulb1OnOff() { // hàm bắt sự kiện click đèn 1
    if(checkBulb1 === 0)
    {
        await putRestApi(1,1);
        color1[0].style.color = 'red' ;
        checkBulb1 = 1;
    }
    else
    {
        await putRestApi(1,2);
        color1[0].style.color = '';
        checkBulb1 = 0;
    }
}
// bat tat den 2
async function bulb2OnOff() { // hàm bắt sự kiện click đèn 2
    if(checkBulb2 === 0)
    {
        await putRestApi(1,3);
        color2[0].style.color = 'red' ;
        checkColor2 = 1;
        checkBulb2 = 1;
    }
    else
    {
        await putRestApi(1,4);
        color2[0].style.color = '';
        checkBulb2 = 0;
    }
}

//click plus minus
var checkColor2;
async function clickPlusBulb2() {
    if (checkBulb2 === 1)
    {
        if (checkColor2 === 1)
        {
           await putRestApi(1,18);
           checkColor2 = 2;
           color2[0].style.color = 'green';
        }
        else if (checkColor2 === 2)
        {
           await putRestApi(1,19);
           checkColor2 = 3;
           color2[0].style.color = 'blue';
        }
        else if (checkColor2 === 3)
        {
           await putRestApi(1,3);
           checkColor2 = 1;
           color2[0].style.color = 'red';
        }
    }
}
async function clickMinusBulb2() {
    if (checkBulb2 === 1)
    {
        if (checkColor2 === 1)
        {
           await putRestApi(1,19);
           checkColor2 = 3;
           color2[0].style.color = 'blue';
        }
        else if (checkColor2 === 2)
        {
           await putRestApi(1,3);
           checkColor2 = 1;
           color2[0].style.color = 'red';
        }
        else if (checkColor2 === 3)
        {
           await putRestApi(1,18);
           checkColor2 = 2;
           color2[0].style.color = 'green';
        }
    }
}


/* dieu khien dieu hoa */

// click power
async function clickPowerAir() {
    if(statusAir === 0)
    {
        await putRestApi(1,5);
        statusAir = 1;
        displayAir[0].style.display = 'block'
    }
    else
    {
        await putRestApi(1,6);
        statusAir = 0;
        displayAir[0].style.display = 'none'
    }
}
// click mode
async function clickModeAir() {
    if (modeAir === 0) //  Aoto => heat
    {
        await putRestApi(1,8)
        modeAir = 1;
        document.querySelector('.mode').innerHTML = 'Mode: HEAT'
    }
    else if (modeAir === 1) // heat => cool
    {
        await putRestApi(1,9)
        modeAir = 2;
        document.querySelector('.mode').innerHTML = 'Mode: COOL'
    }
    else if (modeAir === 2) // cool => dry
    {
        await putRestApi(1,10)
        modeAir = 3;
        document.querySelector('.mode').innerHTML = 'Mode: DRY'
    }
    else if (modeAir === 3) // dry => fan
    {
        await putRestApi(1,23)
        modeAir = 4;
        document.querySelector('.mode').innerHTML = 'Mode: FAN'
    }
    else if (modeAir === 4) // fan => auto
    {
        await putRestApi(1,7)
        modeAir = 0;
        document.querySelector('.mode').innerHTML = 'Mode: AUTO'
    }
}
// click speed
async function clickSpeedAir() {
    if(speedAir === 0) {
        await putRestApi(1,25)
        speedAir = 1
        document.querySelector('.speed').innerHTML = 'Speed Fan: 1'
    }
    else if(speedAir === 1) {
        await putRestApi(1,26)
        speedAir = 2
        document.querySelector('.speed').innerHTML = 'Speed Fan: 2'
    }
    else if(speedAir === 2) {
        await putRestApi(1,27);
        speedAir = 3
        document.querySelector('.speed').innerHTML = 'Speed Fan: 3'
    }
    else if(speedAir === 3) {
        await putRestApi(1,28)
        speedAir = 4
        document.querySelector('.speed').innerHTML = 'Speed Fan: 4'
    }
    else if(speedAir === 4) {
        await putRestApi(1,29)
        speedAir = 5
        document.querySelector('.speed').innerHTML = 'Speed Fan: 5'
    }
    else if(speedAir === 5) {
        await putRestApi(1,24)
        speedAir = 0
        document.querySelector('.speed').innerHTML = 'Speed Fan: Auto'
    }
}
// click up-down
async function clickUpAir() {
    if(tempAir < 30)
    {
        await setTempAir(tempAir+1)
        tempAir += 1;
        document.querySelector('.temp').innerHTML = `${tempAir}`
    }
}
async function clickDownAir() {
    if (tempAir > 16)
    {
        await setTempAir(tempAir-1)
        tempAir -= 1;
        document.querySelector('.temp').innerHTML = `${tempAir}`
    }
}
// ham set nhiet độ lưu trong database cuar điều hòa
async function setTempAir(temp) {
    if (temp >= 16 && temp <= 30){
        var urlSetTempAir = url + `settempair/1?temp=${temp}` // tang giá trị nhiet do trong database len 1
    await fetchData(urlSetTempAir,"PUT")
    .then(res => res.json())
    .then(data => data)
    .catch(error => error)
    }
}


/* dieu khien loa */
// click power
async function clickPowerSpeak() {
    if(statusSpeak === 0)
    {
        await putRestApi(1,20);
        await getStatusSpeak();
    }
    else
    {
        await putRestApi(1,21);
        await getStatusSpeak();
    }
}

// click play/pauce
async function clickPlayPauce() {
    if (playPauseSpeak === 0)
    {
        await putRestApi(1,11);
        playPauseSpeak = 1;
        document.querySelector('.play_pause_stop').innerHTML = "Play";
    }
    else 
    {
        await putRestApi(1,12);
        playPauseSpeak = 0;
        document.querySelector('.play_pause_stop').innerHTML = "Pause";
    }
}
// click Stop
async function clickStop() {
    if (volumeSpeak === 1)
    {
        await putRestApi(1,15);
        volumeSpeak = 0;
        volumeMax[0].style.display = 'none';
        volumeMute[0].style.display = 'block';
    }
    else{
        await putRestApi(1,22);
        volumeSpeak = 1;
        volumeMax[0].style.display = 'block';
        volumeMute[0].style.display = 'none';
    }
}
// click next / back
async function clickNext() {
    await getKeyRemote();
    if (checkKeyRemote === 16)
    {
        await putRestApi(1,14);
        displaySpeakClickNext()
        setTimeout(displaySpeakClickOfNext,300)
    }
    else
    {
        await putRestApi(1,16)
        displaySpeakClickNext()
        setTimeout(displaySpeakClickOfNext,300)
    }
}
async function clickBack() {
    await getKeyRemote();
    if (checkKeyRemote === 13)
    {
        await putRestApi(1,17);
        displaySpeakClick()
        setTimeout(displaySpeakClickOff,300);
    }
    else
    {
        await putRestApi(1,13);
        displaySpeakClick()
        setTimeout(displaySpeakClickOff,300);  
    }
}


// dispay speak

// display when click back
function displaySpeakClick() {
    const displayspeakclick = document.getElementsByClassName("icon_back_speak_2");
    displayspeakclick[0].style.color = 'red';
    displayspeakclick[0].style.fontSize = '20px';
}
function displaySpeakClickOff() {
    const displayspeakclick = document.getElementsByClassName("icon_back_speak_2");
    displayspeakclick[0].style.color = '';
    displayspeakclick[0].style.fontSize = '15px';
}

// display when click next next
function displaySpeakClickNext() {
    const clicknextdisplay = document.getElementsByClassName("icon_next_speak_2");
    clicknextdisplay[0].style.color = 'red';
    clicknextdisplay[0].style.fontSize = '20px';
}
function displaySpeakClickOfNext() {
    const clicknextdisplay = document.getElementsByClassName("icon_next_speak_2");
    clicknextdisplay[0].style.color = '';
    clicknextdisplay[0].style.fontSize = '15px';
}
 


/** Fetch API */
async function putRestApi(a,b){ 
    var urlKeyRemote = url + `setkeyremote/${a}?key=${b}` 
    await fetchData(urlKeyRemote,"PUT")
    .then(res => res.json()) // data nhận lại dạng Json
    .then(data => data) // data nhận lại dạng string
    .catch(error => error)
}


// get keyRemote
var checkKeyRemote ;
async function getKeyRemote(){
    var urlgetkeyremote = url + 'getkeyremote/1' // lấy key remote trong database
    await fetchData(urlgetkeyremote,"GET")
    .then(res => res.json())
    .then(data => checkKeyRemote = data)
    .catch(error => error)
}
//setInterval(getKeyRemote,100)   // lấy key remote sau mỗi 100 ms


// đồng bộ (các client khi truy cập vào giao diện remote đều hiển thị các trạng thái thiết bị như nhau)
// Bulb 1
var color1 = document.getElementsByClassName('bulb1');
var checkBulb1 ; 
var urlStatus1 = url + 'getstatus/1'; // lấy trạng thái của đèn 1 trong database
async function checkStatusBulb1()  {
    await fetchData(urlStatus1,"GET")
    .then(res => res.json())
    .then(data => {data === 1? color1[0].style.color = 'red' : color1[0].style.color = '';checkBulb1 = data}) // nếu giá trị trong db = 1 thì hiển thị đèn sáng đỏ,db = 0 đèn tắt
    .catch(error => error)
}
//setInterval(checkStatusBulb1,100);  // check trạng thái của đèn 1 mỗi 100ms

// bulb2
var checkBulb2 ;
var urlStatus2 = url + 'getstatus/2'; // lấy trạng thái của đèn 2 trong db
async function checkStatusBulb2()  {
    await fetchData(urlStatus2,"GET")
    .then(res => res.json())
    .then(data => checkBulb2=data)
    .catch(error => error)
}
//setInterval(checkStatusBulb2,100);

var color2 = document.getElementsByClassName('bulb2');
var urlColor2 = url + 'getcolor/2'; // lấy giá trị màu sắc của đèn 2 trong db
async function checkColorBulb2() {
    await fetchData(urlColor2,"GET")
    .then(res => res.json())
    .then(data => {if(data === 0 ){color2[0].style.color = ''};if(data === 1 ){color2[0].style.color = 'red'};if(data === 2){color2[0].style.color = 'green'}; if(data === 3){color2[0].style.color = 'blue'};checkColor2 = data})
    .catch(error => error)
}
//setInterval(checkColorBulb2,100); 


// get statusSpeak 
var idSpeak = 1;
var statusSpeak ;
var playPauseSpeak ;
var volumeSpeak;
var urlgetStatusSpeak = url + 'getstatusspeak/1';
async function getStatusSpeak() {
     await fetchData(urlgetStatusSpeak,"GET")
    .then(res => res.json())
    .then(data => {idSpeak = data.id;statusSpeak = data.status;playPauseSpeak = data.playPause;volumeSpeak = data.volume})   //data = {"id":1,"status":0,"playPause":0,"volume":1}
    .catch(error => error) 
    statusSpeak === 1 ? displaySpeak[0].style.display = 'block' : displaySpeak[0].style.display = 'none';
    document.querySelector('.play_pause_stop').innerHTML = playPauseSpeak === 1 ? "Play" : "Pause";
    if (volumeSpeak === 0){volumeMax[0].style.display = 'none';volumeMute[0].style.display = 'block'}
    if (volumeSpeak === 1){volumeMax[0].style.display = 'block';volumeMute[0].style.display = 'none'}
}
 //setInterval(getStatusSpeak,100);

 // get statusAir
 var idAir;
 var statusAir;
 var modeAir;
 var speedAir;
 var tempAir;

 var urlgetStatusAir = url + 'getstatusair/1';
async function getStatusAir() {
     await fetchData(urlgetStatusAir,"GET")
    .then(res => res.json())
    .then(data => {idAir= data.id;statusAir = data.status;modeAir = data.mode;speedAir = data.speed;tempAir = data.temp})   //data = {"id":1,"status":0,"playPause":0,"volume":1}
    .catch(error => error) 
    statusAir === 1 ? displayAir[0].style.display = 'block' : displayAir[0].style.display = 'none';
    if (modeAir === 0){document.querySelector('.mode').innerHTML = 'Mode: AUTO'}
    if (modeAir === 1){document.querySelector('.mode').innerHTML = 'Mode: HEAT'}
    if (modeAir === 2){document.querySelector('.mode').innerHTML = 'Mode: COOL'}
    if (modeAir === 3){document.querySelector('.mode').innerHTML = 'Mode: DRY'}
    if (modeAir === 4){document.querySelector('.mode').innerHTML = 'Mode: FAN'}
    if (speedAir === 0){document.querySelector('.speed').innerHTML = 'Speed Fan: Auto'}
    if (speedAir === 1){document.querySelector('.speed').innerHTML = 'Speed Fan: 1'}
    if (speedAir === 2){document.querySelector('.speed').innerHTML = 'Speed Fan: 2'}
    if (speedAir === 3){document.querySelector('.speed').innerHTML = 'Speed Fan: 3'}
    if (speedAir === 4){document.querySelector('.speed').innerHTML = 'Speed Fan: 4'}
    if (speedAir === 5){document.querySelector('.speed').innerHTML = 'Speed Fan: 5'}
    document.querySelector('.temp').innerHTML = `${tempAir}`
    }
    //setInterval(getStatusAir,100);



// hiển thị thời gian hiện tại
const hours = document.querySelector('#hour')
const minutes = document.querySelector('#minutes')
const seconds = document.querySelector('#seconds')
const period = document.querySelector('#period')
const days = document.querySelector('#day')
const months = document.querySelector('#month')
const years = document.querySelector('#year')
setInterval(async () => {
    var d = new Date();
    var htime = d.getHours();
    var mtime = d.getMinutes();
    var stime = d.getSeconds();
    var pperiod = 'AM';

    var ddate = d.getDate();
    var mmonth = d.getMonth();
    var yyear = d.getFullYear();


    if (htime > 12)
    {
        htime = htime - 12;
        pperiod = 'PM'
    }
    if(htime <10)
    {
        htime = `0${htime}`
    }
    if (stime < 10 )
    {
        stime = `0${stime}`
    }
    if (mtime < 10 )
    {
        mtime = `0${mtime}`
    }

    hours.innerHTML = `${htime}`;
    minutes.innerHTML = `${mtime}`;
    seconds.innerHTML = `${stime}`;
    period.innerHTML = `${pperiod}`;

    mmonth = mmonth +1 ;
    if (ddate < 10)
    {
        ddate = `0${ddate}`
    }
    if (mmonth < 10)
    {
        mmonth = `0${mmonth}`
    }
    days.innerHTML = `${ddate}`;
    months.innerHTML = `${mmonth}`;
    years.innerHTML = `${yyear}`;
}, 1000);

//lay gia tri cua cam bien tia lửa trong database
async function getFire() {
    var urlfire = url + "getkeyfire/1"
    await fetchData(urlfire,"GET")
    .then(res => res.json())
    .then(data => firealarm(data))
    .catch(error => error)
}
setInterval(getFire,2000)
const waringFire = document.getElementsByClassName("firealarm")
const waringFire1 = document.getElementsByClassName("warning")
const waringFire2 = document.getElementsByClassName("warning2")
const backPage = document.getElementById("back_page")
async function firealarm(data){
    if (data === 0)
    {
        await putRestApi(1,6);
        displayAir[0].style.display = 'none'
        waringFire[0].style.display = 'flex';
       // backPage[0].style.display = 'none'
        setTimeout( async () => {
            waringFire1[0].style.display = 'none';
            waringFire2[0].style.display = 'block';
        },1000)
        setTimeout(async () => {
            waringFire2[0].style.display = 'none';
            waringFire1[0].style.display = 'block';
        },1500)
    }
    else
    {
        waringFire[0].style.display = 'none';
       // backPage[0].style.display = 'block'
    }
}

var checkPage = false;
async function PageSwitch() {
    console.log(checkPage)
    checkPage = !checkPage;
    if (checkPage)
    {
        document.getElementsByClassName("container")[0].style.display = "none";
        document.getElementsByClassName("container2")[0].style.display = "block";
    } 
    else
    {
        document.getElementsByClassName("container")[0].style.display = "block";
        document.getElementsByClassName("container2")[0].style.display = "none";
    } 
}

