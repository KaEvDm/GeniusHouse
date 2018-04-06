import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Device } from './device';

@Component({
    templateUrl: './device-list.component.html',
    providers: [DataService]
})
export class DeviceListComponent implements OnInit
{
    devices: Device[];

    constructor(private dataService: DataService) { }

    ngOnInit()
    {
        this.dataService.getDevices().subscribe((data: Device[]) => this.devices = data);
    }
}