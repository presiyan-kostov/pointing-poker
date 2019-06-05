import React from 'react';

export const UserContext = React.createContext({
    authenticatedUser: '',
    updateAuthenticatedUser: () => {},

    messages: [],
    pushNewMessage: () => {},
    removeMessageAt: () => {},
    clearMessages: () => {},
});