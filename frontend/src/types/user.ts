export type User = {
  userId: number;
  username: string;
  email: string;
};

export type CreateUserPayload = {
  username: string;
  roleId: number;
  email: string;
  password: string;
};
