export type Session = {
  token: string;
  refreshToken?: string;
  userId?: number;
  email?: string;
};

export type LoginPayload = {
  email: string;
  password: string;
  tenantCode: string;
};

export type LoginResponse = {
  Success?: boolean;
  Data?: {
    token?: {
      token?: string;
      refreshToken?: string;
      userId?: number;
      email?: string;
    };
    message?: string;
  };
  Message?: string;
};
