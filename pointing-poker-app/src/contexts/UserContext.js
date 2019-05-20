import React from 'react';

export const UserContext = React.createContext({
    authenticatedUserId: '',
    updateAuthenticatedUserId: () => {},

    messages: [],
    pushNewMessage: () => {},
    removeMessageAt: () => {}
});