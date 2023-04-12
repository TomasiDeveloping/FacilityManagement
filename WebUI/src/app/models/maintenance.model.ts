export interface Maintenance {
  id: string;
  interval: number;
  name: string;
  lastExecution: Date;
  modifyDate: Date;
  modifyBy: string;
}
