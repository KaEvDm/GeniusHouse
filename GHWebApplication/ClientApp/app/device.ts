export class Device {
    public infoClass: Info;
    public arrInfoKey: any[];
    constructor(
        public id?: number,
        public name?: string,
        public company?: string,
        public category?: string,
        public room?: string,
        public power?: boolean,
        public info?: string) { }
}

export class Info {
    public brightness?: number;
    public isOn?: boolean;
    public temperature?: number;
    public temperatureSensorReadings?: number;
    public isSecureMod?: boolean;
    public mod?: number;
    public isWater?: boolean;
}


