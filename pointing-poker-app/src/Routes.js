import React, { Suspense, lazy } from 'react';
import { Route, Switch } from "react-router-dom";
import "./Routes.css";

const HomeLazy = lazy(() => import('./containers/Home'));
const NotFoundLazy = lazy(() => import('./containers/NotFound'));
const LoginLazy = lazy(() => import('./containers/Login'));
const SignUpLazy = lazy(() => import('./containers/SignUp'));
const ProjectListLazy = lazy(() => import('./containers/ProjectList'));
const ProjectLazy = lazy(() => import('./containers/Project'));

export default () =>
  <Suspense fallback={
      <div className="Loading">
        <h3>Loading...</h3>
      </div>}>
    <Switch>
      <Route path="/" exact component={HomeLazy} />
      <Route path="/home" exact component={HomeLazy} />
      <Route path="/login" exact component={LoginLazy} />
      <Route path="/signup" exact component={SignUpLazy} />
      <Route path="/projectlist" exact component={ProjectListLazy} />
      <Route path="/project" exact component={ProjectLazy} />
      <Route component={NotFoundLazy} />
    </Switch>
  </Suspense>;