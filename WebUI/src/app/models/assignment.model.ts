export interface Assignment {
  id?: string;
  userId: string;
  name: string;
  description: string;
  isCompleted: boolean;
  createDate?: Date;
}
