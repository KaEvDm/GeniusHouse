import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Device } from './device';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})

export class AppComponent implements OnInit {
    device: Device = new Device();      // изменяемое устройство
    devices: Device[];                  // массив устройств
    tableMode: boolean = true;          // табличный режим

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadDevices();             // загрузка данных при старте компонента  
    }

    // получаем данные через сервис
    loadDevices() {
        this.dataService.getDevices()
            .subscribe((data: Device[]) => this.devices = data);
    }

    // сохранение данных
    save() {
        if (this.device.id == null) {
            this.dataService.createDevice(this.device)
                .subscribe((data: Device) => this.devices.push(data));
        } else {
            this.dataService.updateDevice(this.device)
                .subscribe(data => this.loadDevices());
        }
        this.cancel();
    }

    editDevice(p: Device) {
        this.device = p;
    }

    cancel() {
        this.device = new Device();
        this.tableMode = true;
    }

    delete(p: Device) {
        this.dataService.deleteDevice(p.id)
            .subscribe(data => this.loadDevices());
    }

    add() {
        this.cancel();
        this.tableMode = false;
    }
}