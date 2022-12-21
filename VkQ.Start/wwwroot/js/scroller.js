class Scroller {
    constructor(relativeUri, elements, form) {
        this.uri = relativeUri;
        this.elements = $(elements);
        this.query = new URLSearchParams();
        this.data = $(form);
        this.data.on('change', this.ChangeData);
    }

    Start() {
        this.scroller = new InfiniteAjaxScroll(elements, {
            item: '.element', next: this.Handler, spinner: '.spinner', delay: 600
        });
    }

    async ChangeData() {
        this.scroller.pause();
        this.elements.empty();
        this.query = GetFormData();
        this.scroller.pageIndex = -1;
        this.scroller.resume();
        await this.scroller.next();
    }

    async Handler(pageIndex) {
        try {
            this.query.set('page', pageIndex + 1)
            let data = await GetList(this.uri, this.query);
            this.elements.append(data)
            return true;
        } catch (e) {
            console.log(e);
            return false;
        }
    }
    
    ResetData(){
        let form = this.data[0];
        form.reset()
        form.dispatchEvent(new Event('change'))
    }
}

async function GetList(uri, query) {
    let data = await fetch(uri + '?' + query.toString());
    if (data.status === 200) return await data.text();
    throw new Error(await data.text());
}


function GetFormData(data) {
    let d = new FormData(data);
    let url = new URLSearchParams();
    d.forEach((value, key) => {
        url.set(key, value);
    })
    return url;
}