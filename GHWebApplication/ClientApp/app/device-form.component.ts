import { Component, Input } from '@angular/core';
import { Device } from './device';

@Component({
    selector: "device-form",
    templateUrl: './device-form.component.html'
})

export class DeviceFormComponent {
    @Input() device: Device;
}