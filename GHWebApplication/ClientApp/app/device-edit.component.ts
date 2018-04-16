import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
import { Device } from './device';

@Component({
    templateUrl: './device-edit.component.html'
})

export class DeviceEditComponent implements OnInit {

    id: number;
    device: Device;    // изменяемый объект
    loaded: boolean = false;

    constructor(private dataService: DataService, private router: Router, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }

    ngOnInit() {
        if (this.id)
            this.dataService.getDevice(this.id)
                .subscribe((data: Device) => {
                    this.device = data;
                    if (this.device != null) this.loaded = true;
                });
    }

    save() {
        this.dataService.updateDevice(this.device).subscribe(data => this.router.navigateByUrl("/"));
    }
}