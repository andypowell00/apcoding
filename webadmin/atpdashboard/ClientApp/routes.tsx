import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Weather } from './components/Weather';
import { Counter } from './components/Counter';
import { Event } from './components/Event';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/weather' component={ Weather } />
    <Route path='/events' component={ Event } />
</Layout>;
