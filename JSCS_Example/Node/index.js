
Main();

async function Main(){
    const Wrapper = await require('./wrapper.js')();
    await Wrapper.Connect();
}