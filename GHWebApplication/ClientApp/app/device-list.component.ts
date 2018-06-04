import { Input, Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Device, Info } from './device';

@Component({
    templateUrl: './device-list.component.html',
    providers: [DataService]
})
export class DeviceListComponent implements OnInit {
    dev: any;
    devices: Device[];
    public test?: Info;

    @Input() room: string;

    constructor(private dataService: DataService) { }

    

    ngOnInit() {
        this.load();
        console.log("ngOnInit()", this.devices);
    }

    fillFromJSON(json: string): Info {
        let jsonObj = JSON.parse(json);
        let info = new Info();
        for (var propName in jsonObj) {
            info[propName] = jsonObj[propName];
            info["is" + propName] = true;
            console.log("info[propName]", info[propName]);
        }
        return info;
    }

    load() {
        console.log("метод load()");
        this.dataService.getDevices().subscribe((data: Device[]) => {
            this.devices = data;
            for (let dev of this.devices) {
                dev.infoClass = this.fillFromJSON(dev.info);

                let arr : any = [];
                for (let key in dev.infoClass[0]) {
                    arr.push(key);
                }
                dev.arrInfoKey = arr;
                console.log("dev.arrInfoKey", dev.arrInfoKey);
            }

            console.log("this.devices", this.devices);

        });
        console.log("this.devices", this.devices);
    }

    delete(id: number) {
        this.dataService.deleteDevice(id).subscribe(data => this.load());
    }
}