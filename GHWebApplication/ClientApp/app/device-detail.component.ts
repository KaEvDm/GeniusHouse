import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
import { Device } from './device';

@Component(
    {
        templateUrl: './device-detail.component.html',
        providers: [DataService]
    })

export class DeviceDetailComponent implements OnInit
{

    id: number;
    device: Device;
    loaded: boolean = false;

    constructor(private dataService: DataService, activeRoute: ActivatedRoute)
    {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }

    ngOnInit()
    {
        if (this.id)
            this.dataService.getDevice(this.id)
                .subscribe((data: Device) => { this.device = data; this.loaded = true; });
    }
}