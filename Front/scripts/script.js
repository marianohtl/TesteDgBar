addEventListener('load', () => {
    console.log("Hello Clear Sale! =)");
    
    doFetchGenerateNote();
    doRegistryOrder();
});

async function doFetchGenerateNote() {
    let body =  {id:1};
    console.log(JSON.stringify(body))
    const url = await fetch('http://localhost:5000/api/Order/GenerateNote', {method:"post",body:JSON.stringify(body), headers:{"Content-Type":"application/json"}});
    const json = await url.json();

    let order = {
        discont : json.discount,
        finalPrice : json.finalPrice,
        totalPrice : json.totalPrice,
        data : json.items.map( 
        c => (
            {
            item : c.item,
            amount:c.sheetOrder.map(i => i.amount)[0],
            price:c.price
            }
        )
    )
    }
    console.log(json);
}  



async function doRegistryOrder() {
    let body = 
    {
        IdOrder:1,
        IdMenu:2
    };
    console.log(JSON.stringify(body))
    const url = await fetch('http://localhost:5000/api/Order/ResgistryOrder', {method:"post",body:JSON.stringify(body), headers:{"Content-Type":"application/json"}});
    const json = await url.json();    
    console.log(json);
}  



async function DeleteNote() {
    console.log(JSON.stringify(body))
    const url = await fetch('http://localhost:5000/api/Order/ResetOrder/1', {method:"delete"});
}  