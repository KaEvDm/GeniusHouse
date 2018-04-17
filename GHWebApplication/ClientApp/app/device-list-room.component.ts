import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
import { Device } from './device';

@Component(
    {
        templateUrl: './device-list-room.component.html',
        providers: [DataService]
    })

export class DeviceListRoomComponent implements OnInit {

    room: string;
    devices: Device[];

    constructor(private dataService: DataService, private router: Router, activeRoute: ActivatedRoute) {
        this.room = activeRoute.snapshot.params["room"];
    }

    ngOnInit() {
        this.load();
    }

    load() {
        if (this.room)
            this.dataService.getDevicesFromRoom(this.room)
                .subscribe((data: Device[]) => this.devices = data);
    }
}