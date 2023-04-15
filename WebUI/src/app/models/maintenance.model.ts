export interface Maintenance {
  id: string;
  interval: number;
  name: string;
  description: string;
  nextExecution: Date;
  lastExecution: Date;
  modifyDate: Date;
  modifyBy: string;
}
