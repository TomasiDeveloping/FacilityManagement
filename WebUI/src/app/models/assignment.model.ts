export interface Assignment {
  id?: string;
  userId: string;
  name: string;
  userFullName: string;
  description: string;
  isCompleted: boolean;
  createDate?: Date;
}
