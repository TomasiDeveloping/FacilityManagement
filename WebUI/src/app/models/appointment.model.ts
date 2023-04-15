export interface Appointment{
  id: string;
  userId: string;
  startDate: Date;
  endDate: Date;
  reason: string;
  userFullName: string;
  createdBy: string;
}
